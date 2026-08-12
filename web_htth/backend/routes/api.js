const express = require('express');
const router = express.Router();
const jwt = require('jsonwebtoken');
const db = require('../config/db');
const { jwtRequired, isAdmin } = require('../middleware/auth');
const bankingRouter = require('./banking');
require('dotenv').config();

router.use('/', bankingRouter);

const JWT_SECRET = process.env.JWT_SECRET || 'default_jwt_secret_key_for_development_only';

const getClientIp = (req) => {
    const xForwardedFor = req.headers['x-forwarded-for'];
    if (xForwardedFor) {
        return xForwardedFor.split(',')[0].trim();
    }
    return req.ip || req.connection?.remoteAddress || '';
};

// POST /api/register/
router.post('/register', async (req, res) => {
    const { username, password } = req.body;

    if (!username || !password || username.length < 3 || password.length < 3) {
        return res.json({ success: false, message: 'Tài khoản và mật khẩu phải từ 3 ký tự trở lên!' });
    }

    const usernameRegex = /^[a-z0-9]+$/;
    if (!usernameRegex.test(username)) {
        return res.json({ success: false, message: 'Tên tài khoản chỉ được phép sử dụng chữ thường (a-z) và chữ số (0-9)!' });
    }

    const passwordRegex = /^[a-z0-9]+$/;
    if (!passwordRegex.test(password)) {
        return res.json({ success: false, message: 'Mật khẩu chỉ được phép sử dụng chữ thường (a-z) và chữ số (0-9)!' });
    }

    try {
        const clientIp = getClientIp(req);

        // Check registration limit (max 5 accounts per day per IP)
        const [ipCountRows] = await db.execute(
            'SELECT COUNT(*) AS count FROM ip_register_logs WHERE ip = ? AND created_at >= NOW() - INTERVAL 1 DAY',
            [clientIp]
        );
        const registrationCount = ipCountRows[0]?.count || 0;

        if (registrationCount >= 5) {
            return res.json({ success: false, message: 'Mỗi địa chỉ IP chỉ được phép đăng ký tối đa 5 tài khoản trong 1 ngày!' });
        }

        const [existing] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (existing.length > 0) {
            return res.json({ success: false, message: 'Tài khoản đã tồn tại!' });
        }

        await db.execute(
            `INSERT INTO accounts (
                user, \`pass\`, \`char\`, onl, \`lock\`, status, coin, vip,
                sumamount, tichnap, claimed_milestones, napthe, tongnap,
                vnd, phone, activated, kh, mcs, ip_address, gioithieu,
                admin, active, tichdiem
            ) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)`,
            [
                username, password, '[]', 0, 0, 0, 0, 0,
                0, 0, '', 0, 2000000000,
                0, 0, 0, 0, 0, clientIp || '', 0,
                0, 0, 0
            ]
        );

        // Log the successful registration IP
        await db.execute(
            'INSERT INTO ip_register_logs (ip) VALUES (?)',
            [clientIp]
        );

        return res.json({ success: true, message: 'Đăng ký thành công! Hãy đăng nhập.' });
    } catch (err) {
        console.error('Register error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/login/
router.post('/login', async (req, res) => {
    const { username, password } = req.body;

    if (!username || !password) {
        return res.json({ success: false, message: 'Thiếu tài khoản hoặc mật khẩu!' });
    }

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE user = ? AND `pass` = ?', [username, password]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Sai tài khoản hoặc mật khẩu!' });
        }

        const account = rows[0];
        const token = jwt.sign({ user_id: account.id }, JWT_SECRET, { expiresIn: '2h' });

        return res.json({
            success: true,
            message: 'Đăng nhập thành công',
            token: token
        });
    } catch (err) {
        console.error('Login error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/me/
router.get('/me', jwtRequired, async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại' });
        }

        const account = rows[0];
        let charName = "Chưa tạo nhân vật";
        try {
            if (account.char) {
                const chars = JSON.parse(account.char);
                if (Array.isArray(chars) && chars.length > 0) {
                    charName = chars[0];
                }
            }
        } catch (e) {
            // fallback in case it's not a JSON string or format is custom
        }

        return res.json({
            success: true,
            user: {
                id: account.id,
                username: account.user,
                character: charName,
                server: 'Làng Cối Xay Gió (S1)',
                coin: account.coin,
                status: account.status,
                lock: account.lock
            }
        });
    } catch (err) {
        console.error('Get me error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/activate/
router.post('/activate', jwtRequired, async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Lỗi truy xuất tài khoản' });
        }

        const account = rows[0];
        if (account.status === 1) {
            return res.json({ success: false, message: 'Tài khoản đã được kích hoạt!' });
        }

        const currentCoin = parseInt(account.coin || 0, 10);
        if (currentCoin < 10) {
            return res.json({ success: false, message: `Bạn không đủ Coin để kích hoạt! Cần tối thiểu 10 Coin (hiện có ${currentCoin} Coin). Vui lòng nạp thêm.` });
        }

        await db.execute('UPDATE accounts SET status = 1, coin = coin - 10 WHERE id = ?', [req.jwt_user_id]);
        return res.json({ success: true, message: 'Kích hoạt thành công! Đã trừ 10 Coin phí kích hoạt.' });
    } catch (err) {
        console.error('Activate error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/logout/
router.post('/logout', (req, res) => {
    return res.json({ success: true, message: 'Đăng xuất thành công' });
});

// POST /api/change-password/
router.post('/change-password', jwtRequired, async (req, res) => {
    const { oldPassword, newPassword } = req.body;

    if (!oldPassword || !newPassword || newPassword.length < 3) {
        return res.json({ success: false, message: 'Mật khẩu cũ và mật khẩu mới (từ 3 ký tự trở lên) không được để trống!' });
    }

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE id = ?', [req.jwt_user_id]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Tài khoản không tồn tại!' });
        }

        const account = rows[0];
        if (account.pass !== oldPassword) {
            return res.json({ success: false, message: 'Mật khẩu cũ không chính xác!' });
        }

        await db.execute('UPDATE accounts SET `pass` = ? WHERE id = ?', [newPassword, req.jwt_user_id]);
        return res.json({ success: true, message: 'Đổi mật khẩu thành công!' });
    } catch (err) {
        console.error('Change password error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// ================= ADMIN ROUTING =================

// GET /api/admin/stats (Thống kê tài chính & giao dịch)
router.get('/admin/stats', jwtRequired, isAdmin, async (req, res) => {
    try {
        // Query 1: Tổng tiền nạp, Tổng giao dịch, Thành công, Lỗi, Chờ duyệt
        const [summaryRows] = await db.execute(`
            SELECT 
                COALESCE(SUM(CASE WHEN status IN (1, 2) THEN real_amount ELSE 0 END), 0) AS total_recharged,
                COUNT(*) AS total_txns,
                COALESCE(SUM(CASE WHEN status IN (1, 2) THEN 1 ELSE 0 END), 0) AS success_txns,
                COALESCE(SUM(CASE WHEN status = 3 THEN 1 ELSE 0 END), 0) AS failed_txns,
                COALESCE(SUM(CASE WHEN status = 0 THEN 1 ELSE 0 END), 0) AS pending_txns
            FROM recharge_history
        `);
        const summary = summaryRows[0];

        // Query 2: Xếp hạng top 5 nạp tiền nhiều nhất
        const [topDepositors] = await db.execute(`
            SELECT username, COALESCE(SUM(real_amount), 0) AS total_amount, COUNT(*) AS txn_count
            FROM recharge_history
            WHERE status IN (1, 2)
            GROUP BY username
            ORDER BY total_amount DESC
            LIMIT 5
        `);

        // Query 3: Danh sách 15 giao dịch nạp thẻ gần đây nhất
        const [recentTxns] = await db.execute(`
            SELECT username, amount, real_amount, type, status, created_at, description, request_id, telco, serial, code
            FROM recharge_history
            ORDER BY id DESC
            LIMIT 15
        `);

        return res.json({
            success: true,
            stats: {
                totalRecharged: Number(summary.total_recharged),
                totalTxns: Number(summary.total_txns),
                successTxns: Number(summary.success_txns),
                failedTxns: Number(summary.failed_txns),
                pendingTxns: Number(summary.pending_txns),
                topDepositors,
                recentTxns
            }
        });
    } catch (err) {
        console.error('Admin get stats error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống khi tải thống kê: ${err.message}` });
    }
});

// POST /api/admin/add_coin/
router.post('/admin/add_coin', jwtRequired, isAdmin, async (req, res) => {
    const { username, amount, isDeposit } = req.body;
    const coinAmount = parseInt(amount || 0, 10);
    const countAsDeposit = isDeposit !== false;

    try {
        const [rows] = await db.execute('SELECT coin, sumamount, vip, tichnap FROM accounts WHERE user = ?', [username]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = rows[0];
        const currentBalance = parseInt(acc.coin || 0, 10);
        const newBalance = currentBalance + coinAmount;

        if (countAsDeposit && coinAmount > 0) {
            const actualAmount = coinAmount * 1000;
            const currentSumAmount = parseInt(acc.sumamount || 0, 10);
            const currentVip = parseInt(acc.vip || 0, 10);
            const currentTichNap = parseInt(acc.tichnap || 0, 10);

            const newSumAmount = currentSumAmount + actualAmount;
            const newTichNap = currentTichNap + actualAmount;

            // Calculate VIP
            let calculatedVip = 0;
            if (newSumAmount >= 10000000) calculatedVip = 7;
            else if (newSumAmount >= 5000000) calculatedVip = 6;
            else if (newSumAmount >= 3000000) calculatedVip = 5;
            else if (newSumAmount >= 2000000) calculatedVip = 4;
            else if (newSumAmount >= 1000000) calculatedVip = 3;
            else if (newSumAmount >= 500000) calculatedVip = 2;
            else if (newSumAmount >= 200000) calculatedVip = 1;

            const newVip = Math.max(currentVip, calculatedVip);

            await db.execute(
                'UPDATE accounts SET coin = ?, sumamount = ?, vip = ?, tichnap = ? WHERE user = ?',
                [newBalance, newSumAmount, newVip, newTichNap, username]
            );

            // Record transaction log if available
            try {
                await db.execute(
                    'INSERT INTO transactions (username, type, amount, balance_before, balance_after, description) VALUES (?, ?, ?, ?, ?, ?)',
                    [username, 'deposit', coinAmount, currentBalance, newBalance, `Admin Buff Nạp (+${coinAmount.toLocaleString()} Coin)`]
                );
            } catch (tErr) {
                console.error('Transaction log error (non-fatal):', tErr.message);
            }

            // Emit Socket notification
            const reqIo = req.app.get('io');
            if (reqIo) {
                reqIo.to(`user_${username}`).emit('deposit_success', {
                    username: username,
                    amount: coinAmount,
                    newBalance: newBalance,
                    message: `⚡ Admin vừa Buff Nạp thành công +${coinAmount.toLocaleString()} Coin!`,
                    real_amount: actualAmount
                });
            }

            return res.json({ 
                success: true, 
                message: `Đã Buff Nạp ${coinAmount.toLocaleString()} Coin (Tương đương ${actualAmount.toLocaleString()}đ nạp & VIP ${newVip}) cho ${username}!` 
            });
        } else {
            await db.execute('UPDATE accounts SET coin = ? WHERE user = ?', [newBalance, username]);
            return res.json({ success: true, message: `Đã cộng ${coinAmount.toLocaleString()} Coin cho ${username}!` });
        }
    } catch (err) {
        console.error('Admin add coin error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/reset_tichnap/
router.post('/admin/reset_tichnap', jwtRequired, isAdmin, async (req, res) => {
    try {
        await db.execute("UPDATE accounts SET tichnap = 0, claimed_milestones = ''");
        return res.json({ success: true, message: 'Đã reset tích lũy nạp của toàn bộ tài khoản về 0 thành công!' });
    } catch (err) {
        console.error('Admin reset tichnap error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/reset_tichtieu/
router.post('/admin/reset_tichtieu', jwtRequired, isAdmin, async (req, res) => {
    try {
        await db.execute("UPDATE players SET tichtieu_ruby = 0, claimed_tichtieu_ruby = ''");
        return res.json({ success: true, message: 'Đã reset tích tiêu Ruby của toàn bộ nhân vật về 0 thành công!' });
    } catch (err) {
        console.error('Admin reset tichtieu error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/reset_hangdong/
router.post('/admin/reset_hangdong', jwtRequired, isAdmin, async (req, res) => {
    try {
        await db.execute("UPDATE players SET hangdong_stage = 0");
        return res.json({ success: true, message: 'Đã reset tiến trình Hang Động của toàn bộ nhân vật về 0 thành công!' });
    } catch (err) {
        console.error('Admin reset hangdong error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/accounts/
router.get('/admin/accounts', jwtRequired, isAdmin, async (req, res) => {
    try {
        const page = parseInt(req.query.page) || 1;
        const limit = parseInt(req.query.limit) || 10;
        const search = (req.query.search || '').trim();
        const status = req.query.status || 'all';
        const lock = req.query.lock || 'all';

        // Overall statistics
        const [[{ totalAccounts }]] = await db.execute('SELECT COUNT(*) as totalAccounts FROM accounts');
        const [[{ totalOnline }]] = await db.execute('SELECT COUNT(*) as totalOnline FROM accounts WHERE onl = 1');
        const [[{ totalMembers }]] = await db.execute('SELECT COUNT(*) as totalMembers FROM accounts WHERE status = 1');

        // Build SQL WHERE clause for filtering
        let whereConditions = [];
        let params = [];

        if (search) {
            whereConditions.push('(user LIKE ? OR id = ? OR `char` LIKE ?)');
            params.push(`%${search}%`, search, `%${search}%`);
        }
        if (status === 'active') {
            whereConditions.push('status = 1');
        } else if (status === 'inactive') {
            whereConditions.push('status != 1');
        }
        if (lock === 'banned') {
            whereConditions.push('`lock` = 1');
        } else if (lock === 'normal') {
            whereConditions.push('`lock` != 1');
        }

        const whereSql = whereConditions.length > 0 ? 'WHERE ' + whereConditions.join(' AND ') : '';

        // Filtered count
        const [[{ filteredCount }]] = await db.execute(`SELECT COUNT(*) as filteredCount FROM accounts ${whereSql}`, params);

        const totalPages = Math.max(1, Math.ceil(filteredCount / limit));
        const currentPage = Math.min(Math.max(1, page), totalPages);
        const offset = (currentPage - 1) * limit;

        // Query paginated accounts with string interpolation for numeric limit & offset to prevent mysql driver param binding issues with LIMIT
        const [rows] = await db.execute(
            `SELECT id, user, coin, status, \`lock\`, onl, \`char\` FROM accounts ${whereSql} ORDER BY id DESC LIMIT ${parseInt(limit)} OFFSET ${parseInt(offset)}`,
            params
        );

        const accounts = rows.map(acc => {
            let charName = "Chưa tạo nhân vật";
            try {
                if (acc.char) {
                    const chars = JSON.parse(acc.char);
                    if (Array.isArray(chars) && chars.length > 0) {
                        charName = chars[0];
                    }
                }
            } catch (e) {}
            return {
                id: acc.id,
                user: acc.user,
                coin: acc.coin,
                status: acc.status,
                lock: acc.lock,
                onl: acc.onl,
                charName: charName
            };
        });

        return res.json({ 
            success: true, 
            accounts,
            totalAccounts,
            totalOnline,
            totalMembers,
            filteredCount,
            totalPages,
            currentPage,
            limit
        });
    } catch (err) {
        console.error('Admin get accounts error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/search_user/
router.get('/admin/search_user', jwtRequired, isAdmin, async (req, res) => {
    const { username } = req.query;

    if (!username) {
        return res.json({ success: false, message: 'Thiếu tham số username!' });
    }

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = rows[0];
        return res.json({
            success: true,
            user: {
                id: acc.id,
                username: acc.user,
                password: acc.pass,
                coin: acc.coin,
                status: acc.status,
                lock: acc.lock
            }
        });
    } catch (err) {
        console.error('Admin search user error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/update_user/
router.post('/admin/update_user', jwtRequired, isAdmin, async (req, res) => {
    const { username, action, password } = req.body;

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = rows[0];

        if (action === 'lock') {
            const newLock = acc.lock === 0 ? 1 : 0;
            await db.execute('UPDATE accounts SET `lock` = ? WHERE user = ?', [newLock, username]);
            return res.json({ success: true, message: 'Đã cập nhật trạng thái khóa!' });
        } else if (action === 'activate') {
            const newStatus = acc.status === 1 ? 0 : 1;
            await db.execute('UPDATE accounts SET status = ? WHERE user = ?', [newStatus, username]);
            return res.json({ success: true, message: newStatus === 1 ? 'Đã kích hoạt thành viên!' : 'Đã hủy kích hoạt thành viên!' });
        } else if (action === 'password') {
            if (!password) {
                return res.json({ success: false, message: 'Thiếu mật khẩu mới!' });
            }
            await db.execute('UPDATE accounts SET `pass` = ? WHERE user = ?', [password, username]);
            return res.json({ success: true, message: 'Đã đổi mật khẩu thành công!' });
        }

        return res.json({ success: false, message: 'Hành động không hợp lệ' });
    } catch (err) {
        console.error('Admin update user error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/create_giftcode/
router.post('/admin/create_giftcode', jwtRequired, isAdmin, async (req, res) => {
    const { code, beri, ruby, gioihan, item, thongbao, luotnhap, used, special, is_member } = req.body;

    const beriInt = parseInt(beri || 0, 10);
    const rubyInt = parseInt(ruby || 0, 10);
    const gioihanInt = parseInt(gioihan ?? 1, 10);
    const luotnhapInt = parseInt(luotnhap ?? 0, 10);
    const itemJson = item ?? '[]';
    const thongbaoStr = thongbao ?? '';
    const usedStr = used ?? '';
    const specialStr = special ?? '';
    const isMemberInt = parseInt(is_member || 0, 10);

    if (!code) {
        return res.json({ success: false, message: 'Thiếu mã giftcode!' });
    }

    if (gioihanInt < 1) {
        return res.json({ success: false, message: 'Giới hạn lượt nhập phải lớn hơn 0!' });
    }

    if (luotnhapInt < 0 || luotnhapInt > gioihanInt) {
        return res.json({ success: false, message: 'Lượt đã nhập không hợp lệ!' });
    }

    let parsedItem;
    try {
        parsedItem = JSON.parse(itemJson);
        if (!Array.isArray(parsedItem)) {
            return res.json({ success: false, message: 'Trường item phải là mảng JSON!' });
        }
    } catch {
        return res.json({ success: false, message: 'Trường item không đúng định dạng JSON!' });
    }

    try {
        const [existing] = await db.execute('SELECT * FROM giftcode WHERE giftname = ?', [code]);
        if (existing.length > 0) {
            return res.json({ success: false, message: 'Mã code này đã tồn tại!' });
        }

        await db.execute(
            'INSERT INTO giftcode (giftname, beri, ruby, item, thongbao, luotnhap, gioihan, used, special, is_member) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?)',
            [code, beriInt, rubyInt, itemJson, thongbaoStr, luotnhapInt, gioihanInt, usedStr, specialStr, isMemberInt]
        );

        return res.json({ success: true, message: 'Tạo Giftcode thành công!' });
    } catch (err) {
        console.error('Admin create giftcode error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/giftcodes
router.get('/admin/giftcodes', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT * FROM giftcode ORDER BY id DESC');
        return res.json({ success: true, giftcodes: rows });
    } catch (err) {
        console.error('Admin get giftcodes error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// PUT /api/admin/giftcode/:id
router.put('/admin/giftcode/:id', jwtRequired, isAdmin, async (req, res) => {
    const { id } = req.params;
    const { code, beri, ruby, gioihan, item, thongbao, luotnhap, used, special, is_member } = req.body;

    const beriInt = parseInt(beri || 0, 10);
    const rubyInt = parseInt(ruby || 0, 10);
    const gioihanInt = parseInt(gioihan ?? 1, 10);
    const luotnhapInt = parseInt(luotnhap ?? 0, 10);
    const itemJson = item ?? '[]';
    const thongbaoStr = thongbao ?? '';
    const usedStr = used ?? '';
    const specialStr = special ?? '';
    const isMemberInt = parseInt(is_member || 0, 10);

    if (!code) {
        return res.json({ success: false, message: 'Thiếu mã giftcode!' });
    }

    if (gioihanInt < 1) {
        return res.json({ success: false, message: 'Giới hạn lượt nhập phải lớn hơn 0!' });
    }

    if (luotnhapInt < 0 || luotnhapInt > gioihanInt) {
        return res.json({ success: false, message: 'Lượt đã nhập không hợp lệ!' });
    }

    let parsedItem;
    try {
        parsedItem = JSON.parse(itemJson);
        if (!Array.isArray(parsedItem)) {
            return res.json({ success: false, message: 'Trường item phải là mảng JSON!' });
        }
    } catch {
        return res.json({ success: false, message: 'Trường item không đúng định dạng JSON!' });
    }

    try {
        // Check if code exists on another id
        const [existing] = await db.execute('SELECT * FROM giftcode WHERE giftname = ? AND id != ?', [code, id]);
        if (existing.length > 0) {
            return res.json({ success: false, message: 'Mã code này đã tồn tại ở giftcode khác!' });
        }

        const [result] = await db.execute(
            'UPDATE giftcode SET giftname = ?, beri = ?, ruby = ?, item = ?, thongbao = ?, luotnhap = ?, gioihan = ?, used = ?, special = ?, is_member = ? WHERE id = ?',
            [code, beriInt, rubyInt, itemJson, thongbaoStr, luotnhapInt, gioihanInt, usedStr, specialStr, isMemberInt, id]
        );

        if (result.affectedRows === 0) {
            return res.json({ success: false, message: 'Không tìm thấy Giftcode để cập nhật!' });
        }

        return res.json({ success: true, message: 'Cập nhật Giftcode thành công!' });
    } catch (err) {
        console.error('Admin update giftcode error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// DELETE /api/admin/giftcode/:id
router.delete('/api/admin/giftcode/:id', jwtRequired, isAdmin, async (req, res) => {
    const { id } = req.params;

    try {
        const [result] = await db.execute('DELETE FROM giftcode WHERE id = ?', [id]);
        if (result.affectedRows === 0) {
            return res.json({ success: false, message: 'Không tìm thấy Giftcode để xóa!' });
        }
        return res.json({ success: true, message: 'Xóa Giftcode thành công!' });
    } catch (err) {
        console.error('Admin delete giftcode error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/ranking
router.get('/ranking', async (req, res) => {
    try {
        // 1. Fetch top 10 characters by level
        const levelSql = `
            SELECT 
                name, 
                clazz,
                CASE 
                    WHEN level LIKE '[%]' THEN CAST(JSON_UNQUOTE(JSON_EXTRACT(level, '$[0]')) AS UNSIGNED)
                    WHEN level REGEXP '^[0-9]+$' THEN CAST(level AS UNSIGNED)
                    ELSE 1
                END as level
            FROM players
            ORDER BY level DESC
            LIMIT 10
        `;
        const [levelRows] = await db.execute(levelSql);
        const topLevel = levelRows.map(row => ({
            name: row.name,
            level: parseInt(row.level || 0, 10),
            clazz: row.clazz !== null ? parseInt(row.clazz, 10) : 0
        }));

        // 2. Fetch top 10 characters by PvP point
        const pvpSql = `
            SELECT name, pvppoint, clazz 
            FROM players 
            ORDER BY pvppoint DESC 
            LIMIT 10
        `;
        const [pvpRows] = await db.execute(pvpSql);
        const topPvp = pvpRows.map(row => ({
            name: row.name,
            pvppoint: parseInt(row.pvppoint || 0, 10),
            clazz: row.clazz !== null ? parseInt(row.clazz, 10) : 0
        }));

        // 3. Fetch top 10 donators (Top Nạp)
        const napSql = `
            SELECT user, \`char\`, sumamount 
            FROM accounts 
            ORDER BY sumamount DESC 
            LIMIT 10
        `;
        const [napRows] = await db.execute(napSql);
        const topNap = napRows.map(row => {
            let charName = null;
            try {
                const charArr = JSON.parse(row.char);
                if (Array.isArray(charArr) && charArr.length > 0) {
                    charName = charArr[0];
                }
            } catch (e) {
                // Ignore
            }
            
            let displayName = charName;
            if (!displayName) {
                const userStr = row.user || 'Unknown';
                displayName = userStr.length > 3 ? userStr.substring(0, 3) + '***' : userStr + '***';
            }

            return {
                name: displayName,
                sumamount: parseInt(row.sumamount || 0, 10)
            };
        });

        // 4. Fetch top 10 clans (Top Clan)
        const clanSql = `
            SELECT name, member, xp 
            FROM clan 
            ORDER BY xp DESC 
            LIMIT 10
        `;
        const [clanRows] = await db.execute(clanSql);
        const topClan = clanRows.map(row => {
            let memberCount = 0;
            try {
                const memberArr = JSON.parse(row.member);
                if (Array.isArray(memberArr)) {
                    memberCount = memberArr.length;
                }
            } catch (e) {
                // Ignore
            }

            return {
                name: row.name || 'Chưa Đặt Tên',
                xp: parseInt(row.xp || 0, 10),
                members: memberCount
            };
        });

        return res.json({
            success: true,
            topLevel,
            topPvp,
            topNap,
            topClan
        });
    } catch (err) {
        console.error('Fetch ranking error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/news
router.get('/news', async (req, res) => {
    let page = parseInt(req.query.page, 10) || 1;
    let limit = parseInt(req.query.limit, 10) || 10;
    const search = req.query.search || '';

    if (page < 1) page = 1;
    if (limit < 1) limit = 10;
    const offset = (page - 1) * limit;

    try {
        let countSql = "SELECT COUNT(*) as total FROM news WHERE status = 'published'";
        let selectSql = "SELECT * FROM news WHERE status = 'published'";
        const countParams = [];
        const selectParams = [];

        if (search) {
            countSql += " AND (title LIKE ? OR summary LIKE ?)";
            selectSql += " AND (title LIKE ? OR summary LIKE ?)";
            const searchPattern = `%${search}%`;
            countParams.push(searchPattern, searchPattern);
            selectParams.push(searchPattern, searchPattern);
        }

        selectSql += " ORDER BY published_at DESC, id DESC LIMIT ? OFFSET ?";
        selectParams.push(limit, offset);

        const [countRows] = await db.query(countSql, countParams);
        const total = countRows[0].total;

        const [newsRows] = await db.query(selectSql, selectParams);

        return res.json({
            success: true,
            data: newsRows,
            pagination: {
                page,
                limit,
                total,
                totalPages: Math.ceil(total / limit)
            }
        });
    } catch (err) {
        console.error('Fetch news error:', err);
        return res.status(500).json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/news/:idOrSlug
router.get('/news/:idOrSlug', async (req, res) => {
    const { idOrSlug } = req.params;
    try {
        const sql = "SELECT * FROM news WHERE (slug = ? OR id = ?) AND status = 'published'";
        const queryId = parseInt(idOrSlug, 10) || 0;

        const [rows] = await db.query(sql, [idOrSlug, queryId]);
        if (rows.length === 0) {
            return res.status(404).json({ success: false, message: 'Không tìm thấy bài viết!' });
        }

        return res.json({
            success: true,
            data: rows[0]
        });
    } catch (err) {
        console.error('Fetch news detail error:', err);
        return res.status(500).json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// ================= ADMIN NEWS CRUD =================

// GET /api/admin/news
router.get('/admin/news', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [rows] = await db.query('SELECT * FROM news ORDER BY id DESC');
        return res.json({ success: true, news: rows });
    } catch (err) {
        console.error('Admin get news error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/news
router.post('/admin/news', jwtRequired, isAdmin, async (req, res) => {
    const { title, summary, content, thumbnail, status } = req.body;
    if (!title || !summary || !content) {
        return res.json({ success: false, message: 'Vui lòng nhập đầy đủ Tiêu đề, Tóm tắt và Nội dung!' });
    }

    const slug = title
        .toLowerCase()
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .replace(/[đĐ]/g, 'd')
        .replace(/[^a-z0-9\s-]/g, '')
        .trim()
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-');

    const published_at = status === 'published' ? new Date() : null;

    try {
        const [existing] = await db.query('SELECT id FROM news WHERE slug = ?', [slug]);
        let finalSlug = slug;
        if (existing.length > 0) {
            finalSlug = `${slug}-${Date.now().toString().slice(-4)}`;
        }

        const [result] = await db.query(
            'INSERT INTO news (title, slug, summary, content, thumbnail, status, published_at) VALUES (?, ?, ?, ?, ?, ?, ?)',
            [title, finalSlug, summary, content, thumbnail || null, status || 'draft', published_at]
        );

        return res.json({ success: true, message: 'Tạo bài viết thành công!', id: result.insertId });
    } catch (err) {
        console.error('Admin create news error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// PUT /api/admin/news/:id
router.put('/admin/news/:id', jwtRequired, isAdmin, async (req, res) => {
    const { id } = req.params;
    const { title, summary, content, thumbnail, status } = req.body;
    if (!title || !summary || !content) {
        return res.json({ success: false, message: 'Vui lòng nhập đầy đủ Tiêu đề, Tóm tắt và Nội dung!' });
    }

    const slug = title
        .toLowerCase()
        .normalize('NFD')
        .replace(/[\u0300-\u036f]/g, '')
        .replace(/[đĐ]/g, 'd')
        .replace(/[^a-z0-9\s-]/g, '')
        .trim()
        .replace(/\s+/g, '-')
        .replace(/-+/g, '-');

    try {
        const [existing] = await db.query('SELECT id FROM news WHERE id = ?', [id]);
        if (existing.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy bài viết!' });
        }

        const [duplicate] = await db.query('SELECT id FROM news WHERE slug = ? AND id != ?', [slug, id]);
        let finalSlug = slug;
        if (duplicate.length > 0) {
            finalSlug = `${slug}-${Date.now().toString().slice(-4)}`;
        }

        const [oldRows] = await db.query('SELECT status, published_at FROM news WHERE id = ?', [id]);
        let published_at = oldRows[0].published_at;
        if (status === 'published' && oldRows[0].status !== 'published') {
            published_at = new Date();
        } else if (status === 'draft') {
            published_at = null;
        }

        await db.query(
            'UPDATE news SET title = ?, slug = ?, summary = ?, content = ?, thumbnail = ?, status = ?, published_at = ? WHERE id = ?',
            [title, finalSlug, summary, content, thumbnail || null, status || 'draft', published_at, id]
        );

        return res.json({ success: true, message: 'Cập nhật bài viết thành công!' });
    } catch (err) {
        console.error('Admin update news error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// DELETE /api/admin/news/:id
router.delete('/admin/news/:id', jwtRequired, isAdmin, async (req, res) => {
    const { id } = req.params;
    try {
        const [existing] = await db.query('SELECT id FROM news WHERE id = ?', [id]);
        if (existing.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy bài viết!' });
        }

        await db.query('DELETE FROM news WHERE id = ?', [id]);
        return res.json({ success: true, message: 'Xóa bài viết thành công!' });
    } catch (err) {
        console.error('Admin delete news error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/upload
router.post('/admin/upload', jwtRequired, isAdmin, async (req, res) => {
    const fs = require('fs');
    const path = require('path');
    const { fileName, fileData } = req.body;
    if (!fileName || !fileData) {
        return res.json({ success: false, message: 'Thiếu dữ liệu tệp tin!' });
    }

    try {
        const matches = fileData.match(/^data:([A-Za-z-+\/]+);base64,(.+)$/);
        if (!matches || matches.length !== 3) {
            return res.json({ success: false, message: 'Định dạng dữ liệu ảnh không hợp lệ!' });
        }

        const fileBuffer = Buffer.from(matches[2], 'base64');
        const extension = path.extname(fileName) || '.png';
        const newFileName = `thumb-${Date.now()}${extension}`;

        const uploadDir = path.join(__dirname, '../uploads');
        if (!fs.existsSync(uploadDir)) {
            fs.mkdirSync(uploadDir, { recursive: true });
        }

        const filePath = path.join(uploadDir, newFileName);
        fs.writeFileSync(filePath, fileBuffer);

        const protocol = req.headers['x-forwarded-proto'] || req.protocol;
        const host = req.get('host');
        const fileUrl = `${protocol}://${host}/uploads/${newFileName}`;
        return res.json({ success: true, url: fileUrl });
    } catch (err) {
        console.error('File upload error:', err);
        return res.json({ success: false, message: `Lỗi lưu tệp tin: ${err.message}` });
    }
});

// GET /api/recharge/bank_config (Lấy cấu hình ngân hàng công khai)
router.get('/recharge/bank_config', async (req, res) => {
    return res.json({
        success: true,
        bankId: process.env.BANK_ID || 'MB',
        accountNo: process.env.BANK_ACCOUNT_NO || process.env.BANK_ACCOUNT || '123456789999',
        accountName: process.env.BANK_ACCOUNT_NAME || process.env.BANK_OWNER || 'NGUYEN VAN A',
        bankName: process.env.BANK_NAME || `${process.env.BANK_ID || 'MB'} Bank`,
        momoPhone: process.env.MOMO_PHONE || '0987654321',
        momoName: process.env.MOMO_NAME || 'NGUYEN VAN A',
    });
});



module.exports = router;

