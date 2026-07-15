const express = require('express');
const router = express.Router();
const db = require('../config/db');
const { jwtRequired, isAdmin } = require('../middleware/auth');
const PayOS = require('@payos/node');
const { sendAdminDepositNotification } = require('../utils/mailer');
require('dotenv').config();

// Initialize PayOS SDK
let payos = null;
if (process.env.PAYOS_CLIENT_ID && process.env.PAYOS_API_KEY && process.env.PAYOS_CHECKSUM_KEY) {
    try {
        payos = new PayOS(
            process.env.PAYOS_CLIENT_ID,
            process.env.PAYOS_API_KEY,
            process.env.PAYOS_CHECKSUM_KEY
        );
        console.log('PayOS SDK initialized successfully.');
    } catch (err) {
        console.error('Failed to initialize PayOS SDK:', err.message);
    }
} else {
    console.warn('PayOS credentials missing in environment variables. Fallback VietQR will be used.');
}

/**
 * Shared function to handle atomic credit, transaction log, and realtime socket notify.
 * Safe against Race Conditions using SELECT ... FOR UPDATE inside a transaction.
 */
async function processCompletedPayment(lookupVal, actualAmount, reference, gatewayType, io, isRequestId = false) {
    const connection = await db.getConnection();
    try {
        await connection.beginTransaction();
        
        let sql = 'SELECT * FROM recharge_history WHERE code = ? AND status = 0 LIMIT 1 FOR UPDATE';
        if (isRequestId) {
            sql = 'SELECT * FROM recharge_history WHERE request_id = ? AND status = 0 LIMIT 1 FOR UPDATE';
        }
        
        const [rows] = await connection.execute(sql, [lookupVal]);
        if (rows.length === 0) {
            console.log(`[Banking Webhook] Transaction not found or already processed: lookupVal=${lookupVal}`);
            await connection.rollback();
            connection.release();
            return false;
        }
        
        const deposit = rows[0];
        const username = deposit.username;
        
        // Determine status: 1 if amount matches, 2 if wrong amount
        const status = (actualAmount === deposit.amount) ? 1 : 2;
        const coinAmount = Math.floor(actualAmount / 1000); // Tỷ lệ: 1,000đ = 1 Coin
        const statusDesc = (status === 1) 
            ? `Nạp tiền tự động qua ${gatewayType} thành công (${actualAmount.toLocaleString()}đ → ${coinAmount} Coin)` 
            : `Nạp thành công sai mệnh giá (Yêu cầu ${deposit.amount}đ, thực nhận ${actualAmount}đ → ${coinAmount} Coin)`;
        
        // 1. Get current balance, sumamount, vip, and tichnap with lock
        const [userRows] = await connection.execute('SELECT coin, sumamount, vip, tichnap FROM accounts WHERE user = ? FOR UPDATE', [username]);
        if (userRows.length === 0) {
            throw new Error(`User not found: ${username}`);
        }
        
        const currentBalance = parseInt(userRows[0].coin || 0, 10);
        const currentSumAmount = parseInt(userRows[0].sumamount || 0, 10);
        const currentVip = parseInt(userRows[0].vip || 0, 10);
        const currentTichNap = parseInt(userRows[0].tichnap || 0, 10);

        const newBalance = currentBalance + coinAmount;
        const newSumAmount = currentSumAmount + actualAmount;
        const newTichNap = currentTichNap + actualAmount;

        // Calculate VIP level based on newSumAmount
        let calculatedVip = 0;
        if (newSumAmount >= 10000000) {
            calculatedVip = 7;
        } else if (newSumAmount >= 5000000) {
            calculatedVip = 6;
        } else if (newSumAmount >= 3000000) {
            calculatedVip = 5;
        } else if (newSumAmount >= 2000000) {
            calculatedVip = 4;
        } else if (newSumAmount >= 1000000) {
            calculatedVip = 3;
        } else if (newSumAmount >= 500000) {
            calculatedVip = 2;
        } else if (newSumAmount >= 200000) {
            calculatedVip = 1;
        }

        const newVip = Math.max(currentVip, calculatedVip);
        
        // 2. Update user's coin balance, sumamount, vip, and tichnap
        await connection.execute('UPDATE accounts SET coin = ?, sumamount = ?, vip = ?, tichnap = ? WHERE user = ?', [newBalance, newSumAmount, newVip, newTichNap, username]);
        
        // 3. Update deposit history
        await connection.execute(
            'UPDATE recharge_history SET status = ?, real_amount = ?, description = ?, serial = ? WHERE id = ?',
            [status, actualAmount, statusDesc, reference, deposit.id]
        );
        
        // 4. Record balance transaction
        await connection.execute(
            'INSERT INTO transactions (username, type, amount, balance_before, balance_after, description) VALUES (?, ?, ?, ?, ?, ?)',
            [username, 'deposit', coinAmount, currentBalance, newBalance, statusDesc]
        );
        
        await connection.commit();
        connection.release();
        
        console.log(`[Banking Webhook] Successfully processed payment for ${username}: ${actualAmount}đ → +${coinAmount} Coin (status=${status})`);
        
        // 5. Emit Socket.IO event to user's room
        if (io) {
            io.to(`user_${username}`).emit('deposit_success', {
                username: username,
                amount: coinAmount,
                newBalance: newBalance,
                message: statusDesc
            });
            console.log(`[Banking Socket] Emitted success event to user_${username}`);

            // Broadcast global notification to all users online
            io.emit('global_notification', {
                type: 'deposit',
                message: `⚓ Người chơi [${username}] vừa nạp thành công ${coinAmount.toLocaleString()} Coin qua Banking!`
            });
            console.log(`[Banking Socket] Broadcasted global deposit notification`);
        }
        
        return true;
    } catch (err) {
        console.error(`[Banking Webhook] Error processing payment:`, err.message);
        await connection.rollback();
        connection.release();
        return false;
    }
}

// POST /api/banking/deposit - Create a deposit order
router.post('/banking/deposit', jwtRequired, async (req, res) => {
    const { amount } = req.body;
    const depositAmount = parseInt(amount, 10);

    if (isNaN(depositAmount) || depositAmount < 10000) {
        return res.json({ success: false, message: 'Số tiền nạp tối thiểu là 10,000 VNĐ' });
    }

    try {
        // Fetch username
        const [userRows] = await db.execute('SELECT user FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (userRows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại!' });
        }
        const username = userRows[0].user;

        // Generate unique numeric code for Casso/SePay match
        let code;
        let exists = true;
        while (exists) {
            code = Math.floor(100000 + Math.random() * 900000).toString();
            const [rows] = await db.execute("SELECT id FROM recharge_history WHERE code = ? AND status = 0", [code]);
            if (rows.length === 0) exists = false;
        }

        const cleanUsername = username.replace(/[^a-zA-Z0-9]/g, '');
        const transferContent = `WSAC ${code} ${cleanUsername}`.slice(0, 25).trim();
        const requestId = Date.now().toString(); // Numeric request ID for PayOS (using millisecond string)

        // Flow 1: Create PayOS Link if SDK initialized
        let payosUrl = null;
        if (payos) {
            try {
                // orderCode must be a number
                const orderCodeNum = parseInt(requestId.slice(-9) + Math.floor(Math.random() * 100)); // safe integer
                const paymentLink = await payos.createPaymentLink({
                    orderCode: orderCodeNum,
                    amount: depositAmount,
                    description: transferContent,
                    cancelUrl: process.env.PAYOS_CANCEL_URL || 'http://localhost:5173/nap-tien',
                    returnUrl: process.env.PAYOS_RETURN_URL || 'http://localhost:5173/nap-tien'
                });
                payosUrl = paymentLink.checkoutUrl;
                // update request_id with actual orderCode used
                await db.execute(
                    'INSERT INTO recharge_history (username, amount, real_amount, type, status, request_id, code, description) VALUES (?, ?, 0, ?, 0, ?, ?, ?)',
                    [username, depositAmount, 'bank', orderCodeNum.toString(), code, `Đang chờ thanh toán ngân hàng (PayOS/VietQR). Nội dung: ${transferContent}`]
                );
            } catch (payosErr) {
                console.error('PayOS payment link creation failed:', payosErr.message);
            }
        }

        // If PayOS link creation failed or wasn't run, save standard bank record
        if (!payosUrl) {
            await db.execute(
                'INSERT INTO recharge_history (username, amount, real_amount, type, status, request_id, code, description) VALUES (?, ?, 0, ?, 0, ?, ?, ?)',
                [username, depositAmount, 'bank', requestId, code, `Đang chờ thanh toán ngân hàng (VietQR). Nội dung: ${transferContent}`]
            );
        }

        // Fetch bank public details
        const bankConfig = {
            bankId: process.env.BANK_ID || 'MB',
            accountNo: process.env.BANK_ACCOUNT_NO || process.env.BANK_ACCOUNT || '123456789999',
            accountName: process.env.BANK_ACCOUNT_NAME || process.env.BANK_OWNER || 'NGUYEN VAN A'
        };

        // Flow 2: Sinh link VietQR động
        const vietqrUrl = `https://img.vietqr.io/image/${bankConfig.bankId}-${bankConfig.accountNo}-compact2.png?amount=${depositAmount}&addInfo=${encodeURIComponent(transferContent)}&accountName=${encodeURIComponent(bankConfig.accountName)}`;

        // Send email notification to admin asynchronously
        sendAdminDepositNotification(username, depositAmount, transferContent, code);

        return res.json({
            success: true,
            deposit: {
                amount: depositAmount,
                code: code,
                transferContent: transferContent,
                payosUrl: payosUrl,
                vietqrUrl: vietqrUrl
            }
        });
    } catch (err) {
        console.error('Create deposit error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/banking/active - Get the user's currently active deposit order (status = 0 and not expired)
router.get('/banking/active', jwtRequired, async (req, res) => {
    try {
        const [userRows] = await db.execute('SELECT user FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (userRows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại!' });
        }
        const username = userRows[0].user;

        // Fetch bank public details
        const bankConfig = {
            bankId: process.env.BANK_ID || 'MB',
            accountNo: process.env.BANK_ACCOUNT_NO || process.env.BANK_ACCOUNT || '123456789999',
            accountName: process.env.BANK_ACCOUNT_NAME || process.env.BANK_OWNER || 'NGUYEN VAN A'
        };

        const [rows] = await db.execute(
            'SELECT id, amount, code, request_id, status, description, created_at FROM recharge_history WHERE username = ? AND type = "bank" AND status = 0 ORDER BY id DESC LIMIT 1',
            [username]
        );

        if (rows.length === 0) {
            return res.json({ success: true, activeDeposit: null });
        }

        const deposit = rows[0];
        const createdAtTime = new Date(deposit.created_at).getTime();
        const now = Date.now();
        const expiryDuration = 15 * 60 * 1000; // 15 minutes

        if (now - createdAtTime > expiryDuration) {
            // It has expired. Update status to 3 (Failed/Expired) in the database.
            await db.execute(
                'UPDATE recharge_history SET status = 3, description = "Đơn nạp đã hết thời gian thanh toán (15 phút)" WHERE id = ?',
                [deposit.id]
            );
            return res.json({ success: true, activeDeposit: null });
        }

        const cleanUsername = username.replace(/[^a-zA-Z0-9]/g, '');
        const transferContent = `WSAC ${deposit.code} ${cleanUsername}`.slice(0, 25).trim();
        
        // Dynamic VietQR link
        const vietqrUrl = `https://img.vietqr.io/image/${bankConfig.bankId}-${bankConfig.accountNo}-compact2.png?amount=${deposit.amount}&addInfo=${encodeURIComponent(transferContent)}&accountName=${encodeURIComponent(bankConfig.accountName)}`;

        // Reconstruct payosUrl if PayOS is configured and request_id is present
        let payosUrl = null;
        if (payos && deposit.request_id) {
            try {
                const paymentLink = await payos.getPaymentLinkInformation(deposit.request_id);
                if (paymentLink) {
                    payosUrl = paymentLink.checkoutUrl;
                }
            } catch (payosErr) {
                // Not a payos order or error
            }
        }

        return res.json({
            success: true,
            activeDeposit: {
                id: deposit.id,
                amount: deposit.amount,
                code: deposit.code,
                transferContent: transferContent,
                payosUrl: payosUrl,
                vietqrUrl: vietqrUrl,
                created_at: deposit.created_at,
                expires_at: new Date(createdAtTime + expiryDuration).toISOString(),
                status: deposit.status
            }
        });
    } catch (err) {
        console.error('Get active deposit error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/banking/cancel - Cancel a pending deposit order
router.post('/banking/cancel', jwtRequired, async (req, res) => {
    const { code } = req.body;
    if (!code) {
        return res.json({ success: false, message: 'Thiếu mã code đơn nạp' });
    }

    try {
        const [userRows] = await db.execute('SELECT user FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (userRows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại!' });
        }
        const username = userRows[0].user;

        const [rows] = await db.execute(
            'SELECT id FROM recharge_history WHERE code = ? AND username = ? AND status = 0 LIMIT 1',
            [code, username]
        );

        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy đơn nạp đang chờ với mã code này.' });
        }

        // Update status to 4 (Cancelled)
        await db.execute(
            'UPDATE recharge_history SET status = 4, description = ? WHERE id = ?',
            ['Đơn nạp đã bị người dùng hủy', rows[0].id]
        );

        return res.json({ success: true, message: 'Đã hủy đơn nạp thành công.' });
    } catch (err) {
        console.error('Cancel deposit error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống khi hủy: ${err.message}` });
    }
});


// GET /api/banking/history - View user's deposit history
router.get('/banking/history', jwtRequired, async (req, res) => {
    try {
        const [userRows] = await db.execute('SELECT user FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (userRows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại!' });
        }
        const username = userRows[0].user;

        const [rows] = await db.execute(
            'SELECT amount, real_amount, status, code, description, created_at FROM recharge_history WHERE username = ? AND type = "bank" ORDER BY id DESC LIMIT 10',
            [username]
        );
        return res.json({ success: true, history: rows });
    } catch (err) {
        console.error('Get deposit history error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/webhook/payos - Webhook endpoint for PayOS
router.post('/webhook/payos', async (req, res) => {
    console.log('[PayOS Webhook] Received webhook notification.');
    if (!payos) {
        return res.status(400).json({ error: 1, message: 'PayOS SDK is not configured.' });
    }

    try {
        // Verify payment webhook signature and extract data
        const body = req.body;
        const verifiedData = payos.verifyPaymentWebhookData(body);
        
        if (verifiedData) {
            const orderCode = verifiedData.orderCode.toString();
            const amount = verifiedData.amount;
            const reference = verifiedData.reference || `payos_${Date.now()}`;
            
            console.log(`[PayOS Webhook] Signature verified. Order: ${orderCode}, Amount: ${amount}`);
            
            // Process the transaction using orderCode (request_id)
            const processed = await processCompletedPayment(orderCode, amount, reference, 'payos', req.app.get('io'), true);
            
            if (processed) {
                return res.json({ error: 0, message: 'Ok', data: {} });
            } else {
                return res.status(400).json({ error: 1, message: 'Payment processing failed or already processed' });
            }
        } else {
            return res.status(400).json({ error: 1, message: 'Invalid webhook data signature.' });
        }
    } catch (err) {
        console.error('[PayOS Webhook] Verification error:', err.message);
        return res.status(500).json({ error: 1, message: err.message });
    }
});

// POST /api/webhook/sepay-casso - Webhook endpoint for SePay or Casso
router.post('/webhook/sepay-casso', async (req, res) => {
    console.log('[Casso/SePay Webhook] Received webhook notification.', JSON.stringify(req.body));
    
    try {
        const txs = [];
        if (req.body.data && Array.isArray(req.body.data)) {
            // Casso format
            txs.push(...req.body.data);
        } else if (req.body) {
            // SePay format or single Casso item
            txs.push(req.body);
        }

        let transactionsProcessed = 0;

        for (const tx of txs) {
            // SePay/Casso fields mapping
            const description = tx.description || tx.content || tx.transferContent || '';
            const match = description.match(/WSAC\s*(\d+)/i);
            
            if (match) {
                const code = match[1];
                const amount = parseInt(tx.amount || tx.transferAmount || 0, 10);
                const reference = tx.id || tx.tid || tx.referenceCode || `txn_${Date.now()}`;
                
                console.log(`[Casso/SePay Webhook] Found matching memo: Code=${code}, Amount=${amount}, Ref=${reference}`);
                
                const processed = await processCompletedPayment(code, amount, reference, 'bank_transfer', req.app.get('io'), false);
                if (processed) {
                    transactionsProcessed++;
                }
            } else {
                console.log(`[Casso/SePay Webhook] Memo did not match WSAC code: "${description}"`);
            }
        }

        return res.json({ success: true, message: `Processed ${transactionsProcessed} transactions.` });
    } catch (err) {
        console.error('[Casso/SePay Webhook] Error:', err.message);
        return res.status(500).json({ success: false, message: err.message });
    }
});

// ================= ADMIN BANKING MANAGEMENT =================

// GET /api/admin/banking/orders - List all bank deposit orders
router.get('/admin/banking/orders', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [rows] = await db.execute(
            'SELECT id, username, amount, real_amount, code, request_id, status, description, created_at FROM recharge_history WHERE type = "bank" ORDER BY id DESC LIMIT 100'
        );
        return res.json({ success: true, orders: rows });
    } catch (err) {
        console.error('Admin get banking orders error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/banking/pending - List all pending bank deposits
router.get('/admin/banking/pending', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [rows] = await db.execute(
            'SELECT id, username, amount, code, request_id, description, created_at FROM recharge_history WHERE type = "bank" AND status = 0 ORDER BY id DESC'
        );
        return res.json({ success: true, pending: rows });
    } catch (err) {
        console.error('Admin get pending deposits error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/banking/approve - Approve a pending deposit and credit coin
router.post('/admin/banking/approve', jwtRequired, isAdmin, async (req, res) => {
    const { code, amount } = req.body;
    const actualAmount = parseInt(amount, 10);

    if (!code || isNaN(actualAmount) || actualAmount <= 0) {
        return res.json({ success: false, message: 'Thiếu mã code nạp hoặc số tiền không hợp lệ' });
    }

    try {
        console.log(`[Admin Approve] Approving payment for Code=${code}, Amount=${actualAmount}`);
        const reference = `ADMIN_${Math.random().toString(36).substr(2, 9).toUpperCase()}`;
        const processed = await processCompletedPayment(code, actualAmount, reference, 'admin_approve', req.app.get('io'), false);
        
        if (processed) {
            return res.json({ success: true, message: `Duyệt thành công! Đã cộng ${actualAmount.toLocaleString()} Coin cho tài khoản.` });
        } else {
            return res.json({ success: false, message: 'Không thể duyệt đơn nạp. Đơn có thể đã được xử lý hoặc không tìm thấy.' });
        }
    } catch (err) {
        console.error('Admin approve payment error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống khi duyệt: ${err.message}` });
    }
});

// POST /api/admin/banking/reject - Reject a pending deposit
router.post('/admin/banking/reject', jwtRequired, isAdmin, async (req, res) => {
    const { code } = req.body;

    if (!code) {
        return res.json({ success: false, message: 'Thiếu mã code đơn nạp' });
    }

    try {
        const [rows] = await db.execute(
            'SELECT id, username FROM recharge_history WHERE code = ? AND status = 0 LIMIT 1',
            [code]
        );

        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy đơn nạp đang chờ với mã code này.' });
        }

        await db.execute(
            'UPDATE recharge_history SET status = 3, description = ? WHERE id = ?',
            ['Đơn nạp bị từ chối bởi Admin', rows[0].id]
        );

        console.log(`[Admin Reject] Rejected deposit ID=${rows[0].id} for user ${rows[0].username}`);
        return res.json({ success: true, message: `Đã từ chối đơn nạp của ${rows[0].username}.` });
    } catch (err) {
        console.error('Admin reject payment error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống khi từ chối: ${err.message}` });
    }
});

// POST /api/admin/banking/simulate - Legacy simulate endpoint (kept for compatibility)
router.post('/admin/banking/simulate', jwtRequired, isAdmin, async (req, res) => {
    const { code, amount } = req.body;
    const actualAmount = parseInt(amount, 10);

    if (!code || isNaN(actualAmount)) {
        return res.json({ success: false, message: 'Thiếu mã code nạp hoặc số tiền không hợp lệ' });
    }

    try {
        const reference = `SIM_${Math.random().toString(36).substr(2, 9).toUpperCase()}`;
        const processed = await processCompletedPayment(code, actualAmount, reference, 'simulated_bank', req.app.get('io'), false);
        
        if (processed) {
            return res.json({ success: true, message: `Duyệt nạp tiền thành công! Đã cộng ${actualAmount.toLocaleString()} Coin.` });
        } else {
            return res.json({ success: false, message: 'Không thể xử lý đơn nạp. Có thể đơn đã hoàn thành hoặc không tìm thấy mã code.' });
        }
    } catch (err) {
        console.error('Admin simulate payment error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

module.exports = router;
