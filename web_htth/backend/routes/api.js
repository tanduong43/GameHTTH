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
                0, 0, '', 0, 0,
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
                lock: account.lock,
                admin: (account.user === 'admin' || account.admin === 1 || account.admin === '1') ? 1 : 0
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

// Helper trích xuất danh sách tên nhân vật an toàn từ nhiều định dạng dữ liệu (JSON Array, escaped string, raw string, etc.)
const extractCharNames = (rawChar, fallbackNames = []) => {
    const names = new Set();

    if (Array.isArray(fallbackNames)) {
        fallbackNames.forEach(n => {
            if (n && typeof n === 'string' && n.trim() && n.trim() !== 'Chưa tạo nhân vật') {
                names.add(n.trim());
            }
        });
    } else if (typeof fallbackNames === 'string' && fallbackNames.trim() && fallbackNames.trim() !== 'Chưa tạo nhân vật') {
        names.add(fallbackNames.trim());
    }

    if (rawChar) {
        if (typeof rawChar === 'string') {
            try {
                let parsed = JSON.parse(rawChar);
                if (typeof parsed === 'string') {
                    try {
                        parsed = JSON.parse(parsed);
                    } catch (e) {}
                }
                if (Array.isArray(parsed)) {
                    parsed.forEach(item => {
                        if (item && typeof item === 'string' && item.trim()) {
                            names.add(item.trim());
                        }
                    });
                } else if (typeof parsed === 'string' && parsed.trim()) {
                    names.add(parsed.trim());
                }
            } catch (e) {}
        } else if (Array.isArray(rawChar)) {
            rawChar.forEach(item => {
                if (item && typeof item === 'string' && item.trim()) {
                    names.add(item.trim());
                }
            });
        }

        // Fallback: trích xuất regex loại bỏ dấu ngoặc, nháy, gạch chéo
        if (names.size === 0 && typeof rawChar === 'string') {
            const cleaned = rawChar.replace(/[\[\]"'\\]/g, '').trim();
            if (cleaned) {
                cleaned.split(',').forEach(s => {
                    const trimmed = s.trim();
                    if (trimmed && trimmed !== 'Chưa tạo nhân vật') names.add(trimmed);
                });
            }
        }
    }

    return Array.from(names);
};

// Hàm xử lý Xóa Triệt Để Cả Tài Khoản & Toàn Bộ Nhân Vật Liên Quan
const deleteAccountAndCharacters = async (targetIdentifier, reqJwtUserId, extraCharName = null) => {
    if (!targetIdentifier) {
        return { success: false, message: 'Thiếu thông tin tài khoản hoặc nhân vật cần xóa!' };
    }

    // 1. Tìm tài khoản theo username hoặc id
    let acc = null;
    const [accRows] = await db.execute(
        'SELECT * FROM accounts WHERE user = ? OR id = ? LIMIT 1',
        [targetIdentifier, targetIdentifier]
    );
    if (accRows.length > 0) {
        acc = accRows[0];
    } else {
        // Thử tìm tài khoản sở hữu nhân vật có tên targetIdentifier
        const [accByChar] = await db.execute(
            'SELECT * FROM accounts WHERE JSON_VALID(`char`) AND JSON_CONTAINS(`char`, JSON_QUOTE(?)) LIMIT 1',
            [targetIdentifier]
        );
        if (accByChar.length > 0) {
            acc = accByChar[0];
        } else {
            const [accByLike] = await db.execute(
                'SELECT * FROM accounts WHERE `char` LIKE ? LIMIT 1',
                [`%${targetIdentifier}%`]
            );
            if (accByLike.length > 0) {
                acc = accByLike[0];
            }
        }
    }

    // 2. Nếu tìm thấy tài khoản, kiểm tra quyền Admin
    if (acc) {
        if (acc.admin === 1 || (acc.user && acc.user.toLowerCase() === 'admin') || acc.id === reqJwtUserId) {
            return { success: false, message: 'Không thể xóa tài khoản Quản Trị Viên (Admin)!' };
        }
    }

    // 3. Thu thập danh sách tên nhân vật cần xóa
    const charNamesSet = new Set();

    if (acc) {
        const extracted = extractCharNames(acc.char, [extraCharName, acc.user]);
        extracted.forEach(n => charNamesSet.add(n));
    }

    if (extraCharName && typeof extraCharName === 'string' && extraCharName.trim() && extraCharName.trim() !== 'Chưa tạo nhân vật') {
        charNamesSet.add(extraCharName.trim());
    }

    // Thêm chính targetIdentifier (nếu là nhân vật mồ côi hoặc trùng tên)
    if (typeof targetIdentifier === 'string' && targetIdentifier.trim()) {
        charNamesSet.add(targetIdentifier.trim());
    }

    // Tìm thêm tất cả nhân vật trong players khớp tên tài khoản hoặc các tên đã thu thập
    try {
        const potentialNames = Array.from(charNamesSet);
        if (potentialNames.length > 0) {
            const placeholders = potentialNames.map(() => '?').join(', ');
            const [matchedPlayers] = await db.execute(
                `SELECT name FROM players WHERE name IN (${placeholders})`,
                potentialNames
            );
            matchedPlayers.forEach(p => {
                if (p.name) charNamesSet.add(p.name);
            });
        }
    } catch (e) {
        console.error('Error finding matching players:', e);
    }

    // 4. Nếu không có cả account lẫn player
    if (!acc) {
        // Kiểm tra xem có player nào khớp không
        const [existingPlayers] = await db.execute(
            'SELECT name FROM players WHERE name = ? LIMIT 1',
            [targetIdentifier]
        );
        if (existingPlayers.length === 0) {
            return { success: false, message: `Không tìm thấy tài khoản hoặc nhân vật "${targetIdentifier}" để xóa!` };
        }
        existingPlayers.forEach(p => charNamesSet.add(p.name));
    }

    const charList = Array.from(charNamesSet);
    const deletedChars = [];

    // 5. Dọn dẹp từng nhân vật trong players, clone_char, clan, market
    for (const cName of charList) {
        try {
            const [delPl] = await db.execute('DELETE FROM players WHERE name = ?', [cName]);
            if (delPl.affectedRows > 0) {
                deletedChars.push(cName);
            }
        } catch (e) {
            console.error(`Error deleting player ${cName}:`, e);
        }

        try {
            await db.execute('DELETE FROM clone_char WHERE name = ?', [cName]);
        } catch (e) {}

        // Dọn dẹp trong clan
        try {
            const [clanRows] = await db.execute('SELECT id, member FROM clan');
            for (const cl of clanRows) {
                if (cl.member) {
                    try {
                        const members = JSON.parse(cl.member);
                        if (Array.isArray(members)) {
                            const filtered = members.filter(m => {
                                if (Array.isArray(m) && m.length > 0) {
                                    return String(m[0]).trim() !== cName;
                                }
                                return true;
                            });
                            if (filtered.length !== members.length) {
                                await db.execute('UPDATE clan SET member = ? WHERE id = ?', [JSON.stringify(filtered), cl.id]);
                            }
                        }
                    } catch (errParse) {}
                }
            }
        } catch (clanErr) {
            console.error('Error cleaning clan membership:', clanErr);
        }

        // Dọn dẹp market
        try {
            const [marketRows] = await db.execute('SELECT id, data FROM market');
            for (const mRow of marketRows) {
                if (mRow.data) {
                    try {
                        const mData = JSON.parse(mRow.data);
                        let modified = false;
                        for (const key of ['item3', 'item47']) {
                            if (mData[key]) {
                                const items = typeof mData[key] === 'string' ? JSON.parse(mData[key]) : mData[key];
                                if (Array.isArray(items)) {
                                    const newItems = items.filter(item => {
                                        if (Array.isArray(item)) {
                                            return !item.some(val => String(val).trim() === cName);
                                        }
                                        return true;
                                    });
                                    if (newItems.length !== items.length) {
                                        mData[key] = typeof mData[key] === 'string' ? JSON.stringify(newItems) : newItems;
                                        modified = true;
                                    }
                                }
                            }
                        }
                        if (modified) {
                            await db.execute('UPDATE market SET data = ? WHERE id = ?', [JSON.stringify(mData), mRow.id]);
                        }
                    } catch (errM) {}
                }
            }
        } catch (marketErr) {
            console.error('Error cleaning market listings:', marketErr);
        }
    }

    // 6. Xóa tài khoản trong accounts (nếu có)
    let deletedAccountUser = null;
    if (acc) {
        await db.execute('DELETE FROM accounts WHERE id = ?', [acc.id]);
        deletedAccountUser = acc.user;
    }

    // 7. Tạo thông báo kết quả chi tiết
    let msg = '';
    if (deletedAccountUser && deletedChars.length > 0) {
        msg = `Đã xóa vĩnh viễn tài khoản "${deletedAccountUser}" và nhân vật (${deletedChars.join(', ')}) thành công!`;
    } else if (deletedAccountUser) {
        msg = `Đã xóa vĩnh viễn tài khoản "${deletedAccountUser}" thành công! (Tài khoản chưa có nhân vật)`;
    } else if (deletedChars.length > 0) {
        msg = `Đã xóa vĩnh viễn nhân vật mồ côi "${deletedChars.join(', ')}" khỏi cơ sở dữ liệu thành công!`;
    } else {
        msg = `Đã xóa thành công!`;
    }

    return {
        success: true,
        message: msg,
        account: deletedAccountUser,
        characters: deletedChars
    };
};

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

        // Query 4: Top 10 tài khoản Tiêu Ruby nhiều nhất
        const [spentRubyRows] = await db.execute(`
            SELECT 
                a.id AS account_id,
                a.user AS username,
                GROUP_CONCAT(p.name SEPARATOR ', ') AS char_names,
                SUM(GREATEST(COALESCE(p.tieu_ruby, 0), COALESCE(p.tichtieu_ruby, 0))) AS amount
            FROM accounts a
            JOIN players p ON (JSON_VALID(a.char) AND JSON_CONTAINS(a.char, JSON_QUOTE(p.name)))
            GROUP BY a.id, a.user
            HAVING amount > 0
            ORDER BY amount DESC, a.id ASC
            LIMIT 10
        `);
        const topSpentRuby = spentRubyRows.map(row => ({
            account_id: row.account_id,
            username: row.username,
            char_names: row.char_names || '',
            amount: Number(row.amount || 0)
        }));

        // Query 5: Top 10 tài khoản Có Ruby nhiều nhất
        const [holdRubyRows] = await db.execute(`
            SELECT 
                a.id AS account_id,
                a.user AS username,
                GROUP_CONCAT(p.name SEPARATOR ', ') AS char_names,
                SUM(COALESCE(CAST(JSON_UNQUOTE(JSON_EXTRACT(p.point_inven, '$[1]')) AS UNSIGNED), 0)) AS amount
            FROM accounts a
            JOIN players p ON (JSON_VALID(a.char) AND JSON_CONTAINS(a.char, JSON_QUOTE(p.name)))
            GROUP BY a.id, a.user
            HAVING amount > 0
            ORDER BY amount DESC, a.id ASC
            LIMIT 10
        `);
        const topHoldRuby = holdRubyRows.map(row => ({
            account_id: row.account_id,
            username: row.username,
            char_names: row.char_names || '',
            amount: Number(row.amount || 0)
        }));

        // Query 6: Top 10 tài khoản Có Extol nhiều nhất
        const [holdExtolRows] = await db.execute(`
            SELECT 
                a.id AS account_id,
                a.user AS username,
                GROUP_CONCAT(p.name SEPARATOR ', ') AS char_names,
                SUM(COALESCE(CAST(JSON_UNQUOTE(JSON_EXTRACT(p.point_inven, '$[2]')) AS UNSIGNED), 0)) AS amount
            FROM accounts a
            JOIN players p ON (JSON_VALID(a.char) AND JSON_CONTAINS(a.char, JSON_QUOTE(p.name)))
            GROUP BY a.id, a.user
            HAVING amount > 0
            ORDER BY amount DESC, a.id ASC
            LIMIT 10
        `);
        const topHoldExtol = holdExtolRows.map(row => ({
            account_id: row.account_id,
            username: row.username,
            char_names: row.char_names || '',
            amount: Number(row.amount || 0)
        }));

        // Query 7: Top 10 tài khoản Có Beri nhiều nhất
        const [holdBeriRows] = await db.execute(`
            SELECT 
                a.id AS account_id,
                a.user AS username,
                GROUP_CONCAT(p.name SEPARATOR ', ') AS char_names,
                SUM(COALESCE(CAST(JSON_UNQUOTE(JSON_EXTRACT(p.point_inven, '$[0]')) AS DECIMAL(25, 0)), 0)) AS amount
            FROM accounts a
            JOIN players p ON (JSON_VALID(a.char) AND JSON_CONTAINS(a.char, JSON_QUOTE(p.name)))
            GROUP BY a.id, a.user
            HAVING amount > 0
            ORDER BY amount DESC, a.id ASC
            LIMIT 10
        `);
        const topHoldBeri = holdBeriRows.map(row => ({
            account_id: row.account_id,
            username: row.username,
            char_names: row.char_names || '',
            amount: Number(row.amount || 0)
        }));

        return res.json({
            success: true,
            stats: {
                totalRecharged: Number(summary.total_recharged),
                totalTxns: Number(summary.total_txns),
                successTxns: Number(summary.success_txns),
                failedTxns: Number(summary.failed_txns),
                pendingTxns: Number(summary.pending_txns),
                topDepositors,
                recentTxns,
                topSpentRuby,
                topHoldRuby,
                topHoldExtol,
                topHoldBeri
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
        const [rows] = await db.execute('SELECT coin, sumamount, vip, tichnap, onl FROM accounts WHERE user = ?', [username]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = rows[0];
        const currentBalance = parseInt(acc.coin || 0, 10);
        const newBalance = currentBalance + coinAmount;
        const isUserOnline = parseInt(acc.onl || 0, 10) === 1;

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

            // Cập nhật nguyên tử (Atomic update) để tránh Race Condition với Game Server
            await db.execute(
                'UPDATE accounts SET coin = coin + ?, sumamount = sumamount + ?, tongnap = tongnap + ?, vip = GREATEST(vip, ?), tichnap = tichnap + ? WHERE user = ?',
                [coinAmount, actualAmount, actualAmount, newVip, actualAmount, username]
            );

            // Add Item 360 (Vé tặng 10 ruby, category 4) to player's inventory in `players` table (1k VND = 1 ticket)
            const ticketQuantity = Math.floor(actualAmount / 1000);
            let ticketNote = '';

            if (ticketQuantity > 0) {
                if (isUserOnline) {
                    // Người chơi đang online trong Game Server: RAM của Server Java giữ bag47.
                    // Nếu sửa trực tiếp DB lúc này, khi người chơi logout/chuyển map Server Java sẽ ghi đè RAM xuống DB làm mất đồ.
                    ticketNote = ` (⚠️ Lưu ý: Tài khoản đang ONLINE, không thể thêm trực tiếp ${ticketQuantity} Vé Nạp vào túi để tránh bị Game Server ghi đè mất đồ. Hãy yêu cầu người chơi thoát game để buff đồ!)`;
                    console.warn(`[Admin Buff] Account ${username} is currently ONLINE. Skipped direct bag47 DB write to avoid RAM overwrite loss.`);
                } else {
                    try {
                        const [accRows] = await db.execute('SELECT `char` FROM accounts WHERE user = ? LIMIT 1', [username]);
                        let charName = null;
                        if (accRows.length > 0 && accRows[0].char) {
                            const parsedChar = typeof accRows[0].char === 'string' ? JSON.parse(accRows[0].char) : accRows[0].char;
                            if (Array.isArray(parsedChar) && parsedChar.length > 0) {
                                charName = parsedChar[0];
                            }
                        }
                        if (charName) {
                            const [pRows] = await db.execute('SELECT `bag47` FROM players WHERE name = ? LIMIT 1', [charName]);
                            if (pRows.length > 0) {
                                let bag47 = [];
                                try {
                                    bag47 = typeof pRows[0].bag47 === 'string' ? JSON.parse(pRows[0].bag47) : pRows[0].bag47;
                                } catch (e) {}
                                if (!Array.isArray(bag47)) {
                                    bag47 = [];
                                }

                                let found = false;
                                for (let i = 0; i < bag47.length; i++) {
                                    const entry = typeof bag47[i] === 'string' ? JSON.parse(bag47[i]) : bag47[i];
                                    if (Array.isArray(entry) && entry.length >= 3) {
                                        const cat = parseInt(entry[0], 10);
                                        const itemId = parseInt(entry[1], 10);
                                        if (cat === 4 && itemId === 360) {
                                            entry[2] = parseInt(entry[2], 10) + ticketQuantity;
                                            bag47[i] = entry;
                                            found = true;
                                            break;
                                        }
                                    }
                                }
                                if (!found) {
                                    bag47.push([4, 360, ticketQuantity]);
                                }

                                await db.execute('UPDATE players SET bag47 = ? WHERE name = ?', [JSON.stringify(bag47), charName]);
                                console.log(`[Admin Buff] Added ${ticketQuantity} tickets (Item 360) to player ${charName} (account: ${username})`);
                                ticketNote = ` + ${ticketQuantity} Vé Nạp`;
                            }
                        }
                    } catch (itemErr) {
                        console.error('[Admin Buff] Error adding ticket 360 to player:', itemErr.message);
                    }
                }
            }

            // Record transaction log if available
            try {
                await db.execute(
                    'INSERT INTO transactions (username, type, amount, balance_before, balance_after, description) VALUES (?, ?, ?, ?, ?, ?)',
                    [username, 'deposit', coinAmount, currentBalance, newBalance, `Admin Buff Nạp (+${coinAmount.toLocaleString()} Coin)`]
                );
            } catch (tErr) {
                console.error('Transaction log error (non-fatal):', tErr.message);
            }

            // Record into recharge_history so it has a timestamp for ranking tie-breaker and admin stats
            try {
                const buffRequestId = `ADMIN_BUFF_${Date.now()}`;
                const buffCode = Math.floor(100000 + Math.random() * 900000).toString();
                await db.execute(
                    'INSERT INTO recharge_history (username, amount, real_amount, type, status, request_id, code, description) VALUES (?, ?, ?, ?, ?, ?, ?, ?)',
                    [username, actualAmount, actualAmount, 'admin_buff', 1, buffRequestId, buffCode, `Admin Buff Nạp (+${coinAmount.toLocaleString()} Coin)`]
                );
            } catch (rhErr) {
                console.error('Recharge history log error (non-fatal):', rhErr.message);
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
                message: `Đã Buff Nạp ${coinAmount.toLocaleString()} Coin (Tương đương ${actualAmount.toLocaleString()}đ nạp & VIP ${newVip}) cho ${username}!${ticketNote}` 
            });
        } else {
            await db.execute('UPDATE accounts SET coin = coin + ? WHERE user = ?', [coinAmount, username]);
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

// POST /api/admin/reset_pvp/
router.post('/admin/reset_pvp', jwtRequired, isAdmin, async (req, res) => {
    try {
        await db.execute("UPDATE players SET pvppoint = 0");
        return res.json({ success: true, message: 'Đã reset điểm PVP của toàn bộ nhân vật về 0 thành công!' });
    } catch (err) {
        console.error('Admin reset pvp error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/reset_truyna/
router.post('/admin/reset_truyna', jwtRequired, isAdmin, async (req, res) => {
    try {
        await db.execute("UPDATE players SET wanted_point = 0, point_inven = CASE WHEN JSON_VALID(point_inven) AND JSON_EXTRACT(point_inven, '$[11]') IS NOT NULL THEN JSON_SET(point_inven, '$[11]', 0) ELSE point_inven END");
        return res.json({ success: true, message: 'Đã reset điểm truy nã (bounty) của toàn bộ nhân vật về 0 thành công!' });
    } catch (err) {
        console.error('Admin reset truyna error:', err);
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
        const online = req.query.online || 'all';

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
        if (online === 'online' || online === '1') {
            whereConditions.push('onl = 1');
        } else if (online === 'offline' || online === '0') {
            whereConditions.push('onl != 1');
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
            const charList = extractCharNames(acc.char);
            const charName = charList.length > 0 ? charList.join(', ') : "Chưa tạo nhân vật";
            return {
                id: acc.id,
                user: acc.user,
                coin: acc.coin,
                status: acc.status,
                lock: acc.lock,
                onl: acc.onl,
                charName: charName,
                char: acc.char,
                charList: charList
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
    const { username, action, password, charName, characterName } = req.body;
    const targetUser = username || req.body.id || req.body.target;

    if (!targetUser) {
        return res.json({ success: false, message: 'Thiếu thông tin người dùng!' });
    }

    try {
        if (action === 'delete') {
            const charNameHint = charName || characterName || null;
            const result = await deleteAccountAndCharacters(targetUser, req.jwt_user_id, charNameHint);
            return res.json(result);
        }

        const [rows] = await db.execute('SELECT * FROM accounts WHERE user = ? OR id = ?', [targetUser, targetUser]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = rows[0];

        if (action === 'lock') {
            const newLock = acc.lock === 0 ? 1 : 0;
            await db.execute('UPDATE accounts SET `lock` = ? WHERE id = ?', [newLock, acc.id]);
            return res.json({ success: true, message: 'Đã cập nhật trạng thái khóa!' });
        } else if (action === 'activate') {
            const newStatus = acc.status === 1 ? 0 : 1;
            await db.execute('UPDATE accounts SET status = ? WHERE id = ?', [newStatus, acc.id]);
            return res.json({ success: true, message: newStatus === 1 ? 'Đã kích hoạt thành viên!' : 'Đã hủy kích hoạt thành viên!' });
        } else if (action === 'password') {
            if (!password) {
                return res.json({ success: false, message: 'Thiếu mật khẩu mới!' });
            }
            await db.execute('UPDATE accounts SET `pass` = ? WHERE id = ?', [password, acc.id]);
            return res.json({ success: true, message: 'Đã đổi mật khẩu thành công!' });
        }

        return res.json({ success: false, message: 'Hành động không hợp lệ!' });
    } catch (err) {
        console.error('Admin update user error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/delete_user (Alias)
router.post('/admin/delete_user', jwtRequired, isAdmin, async (req, res) => {
    const { username, charName, characterName } = req.body;
    const target = username || req.body.id || req.body.target;
    const charNameHint = charName || characterName || null;
    const result = await deleteAccountAndCharacters(target, req.jwt_user_id, charNameHint);
    return res.json(result);
});

// GET /api/admin/orphaned_players (Danh sách nhân vật trong players không thuộc account nào)
router.get('/admin/orphaned_players', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [accs] = await db.execute("SELECT `char` FROM accounts");
        const linkedNames = new Set();
        accs.forEach(a => {
            if (a.char) {
                const names = extractCharNames(a.char);
                names.forEach(n => linkedNames.add(n));
            }
        });

        const [allPlayers] = await db.execute("SELECT id, name, level, date, pvppoint, exp FROM players ORDER BY id DESC");
        const orphaned = allPlayers.filter(p => !linkedNames.has(p.name)).map(p => {
            let levelNum = 1;
            try {
                if (p.level) {
                    const parsed = typeof p.level === 'string' ? JSON.parse(p.level) : p.level;
                    if (Array.isArray(parsed) && parsed.length > 0) levelNum = parsed[0];
                }
            } catch (e) {}
            return {
                id: p.id,
                name: p.name,
                level: levelNum,
                date: p.date,
                pvppoint: p.pvppoint || 0,
                exp: p.exp || 0
            };
        });

        return res.json({
            success: true,
            total: orphaned.length,
            count: orphaned.length,
            orphaned,
            orphanedPlayers: orphaned
        });
    } catch (err) {
        console.error('Admin get orphaned players error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/delete_orphaned_players (Xóa một hoặc toàn bộ nhân vật mồ côi)
router.post('/admin/delete_orphaned_players', jwtRequired, isAdmin, async (req, res) => {
    try {
        const deleteAll = req.body.deleteAll || req.body.all;
        const targetNames = req.body.charNames 
            ? (Array.isArray(req.body.charNames) ? req.body.charNames : [req.body.charNames])
            : (req.body.playerName ? [req.body.playerName] : []);

        if (deleteAll) {
            const [accs] = await db.execute("SELECT `char` FROM accounts");
            const linkedNames = new Set();
            accs.forEach(a => {
                if (a.char) {
                    const names = extractCharNames(a.char);
                    names.forEach(n => linkedNames.add(n));
                }
            });

            const [allPlayers] = await db.execute("SELECT name FROM players");
            const toDelete = allPlayers.filter(p => !linkedNames.has(p.name)).map(p => p.name);

            for (const pName of toDelete) {
                await deleteAccountAndCharacters(pName, req.jwt_user_id, pName);
            }

            return res.json({
                success: true,
                message: `Đã dọn dẹp ${toDelete.length} nhân vật mồ côi thành công!`,
                deletedCount: toDelete.length
            });
        }

        if (targetNames.length === 0) {
            return res.json({ success: false, message: 'Thiếu tên nhân vật mồ côi cần xóa!' });
        }

        for (const pName of targetNames) {
            await deleteAccountAndCharacters(pName, req.jwt_user_id, pName);
        }

        return res.json({
            success: true,
            message: `Đã xóa vĩnh viễn ${targetNames.length} nhân vật mồ côi (${targetNames.join(', ')})!`
        });
    } catch (err) {
        console.error('Admin delete orphaned players error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/account_detail
router.get('/admin/account_detail', jwtRequired, isAdmin, async (req, res) => {
    const { username, id } = req.query;

    if (!username && !id) {
        return res.json({ success: false, message: 'Thiếu tham số username hoặc id!' });
    }

    try {
        let accountQuery = 'SELECT * FROM accounts WHERE user = ?';
        let accountParam = [username];
        if (id && !username) {
            accountQuery = 'SELECT * FROM accounts WHERE id = ?';
            accountParam = [id];
        }

        const [accRows] = await db.execute(accountQuery, accountParam);
        if (accRows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = accRows[0];

        // Parse character name
        let charName = null;
        try {
            if (acc.char) {
                const chars = typeof acc.char === 'string' ? JSON.parse(acc.char) : acc.char;
                if (Array.isArray(chars) && chars.length > 0) {
                    charName = chars[0];
                }
            }
        } catch (e) {}

        // Recharge Milestones list definition
        const MILESTONES_CONFIG = [
            { id: 0, num: 50000, label: '50.000đ (50k Extol)' },
            { id: 1, num: 100000, label: '100.000đ (100k Extol)' },
            { id: 2, num: 200000, label: '200.000đ (200k Extol)' },
            { id: 3, num: 300000, label: '300.000đ (300k Extol)' },
            { id: 4, num: 500000, label: '500.000đ (500k Extol)' },
            { id: 5, num: 600000, label: '600.000đ (600k Extol)' },
            { id: 6, num: 1000000, label: '1.000.000đ (1M Extol)' },
            { id: 7, num: 2000000, label: '2.000.000đ (2M Extol)' },
            { id: 8, num: 5000000, label: '5.000.000đ (5M Extol)' }
        ];

        const claimedStr = acc.claimed_milestones || '';
        const claimedSet = new Set(
            claimedStr.split(',').map(s => s.trim()).filter(Boolean).map(Number)
        );

        const milestones = MILESTONES_CONFIG.map((m, idx) => {
            const isClaimed = claimedSet.has(idx) || claimedSet.has(m.id);
            const isReached = (acc.tichnap || 0) >= m.num;
            return {
                ...m,
                isClaimed,
                isReached,
                canClaim: isReached && !isClaimed
            };
        });

        const accountData = {
            id: acc.id,
            user: acc.user,
            coin: acc.coin || 0,
            vip: acc.vip || 0,
            status: acc.status || 0,
            lock: acc.lock || 0,
            onl: acc.onl || 0,
            tichnap: acc.tichnap || 0,
            tongnap: acc.tongnap || acc.sumamount || 0,
            claimed_milestones: claimedStr,
            milestones,
            ip_address: acc.ip_address || 'Không rõ',
            created_at: acc.created_at || null,
            charName: charName,
            extol: acc.vnd || 0,
            vnd: acc.vnd || 0
        };

        let playerData = null;

        if (charName) {
            const [playerRows] = await db.execute('SELECT * FROM players WHERE name = ? LIMIT 1', [charName]);
            if (playerRows.length > 0) {
                const pl = playerRows[0];

                const safeJsonParse = (val, defaultVal = []) => {
                    if (!val) return defaultVal;
                    if (typeof val !== 'string') return val;
                    try {
                        return JSON.parse(val);
                    } catch (e) {
                        return defaultVal;
                    }
                };

                const levelArr = safeJsonParse(pl.level, [1, 0, 0, 0]);
                const pointInvenArr = safeJsonParse(pl.point_inven, [0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0]);
                const siteArr = safeJsonParse(pl.site, [1, 0, 100, 100, 0, 0]);
                const potentialArr = safeJsonParse(pl.potential, [5, 1, 1, 1, 1, 1, 0]);
                const itBodyRaw = safeJsonParse(pl.it_body, []);
                const bag3Raw = safeJsonParse(pl.bag3, []);
                const bag47Raw = safeJsonParse(pl.bag47, []);
                const box3Raw = safeJsonParse(pl.box3, []);
                const box47Raw = safeJsonParse(pl.box47, []);

                // Map name lookup
                const mapId = parseInt(siteArr[0] ?? 1, 10);
                let mapName = `Bản đồ ${mapId}`;
                try {
                    const [mapRows] = await db.execute('SELECT name FROM maps WHERE id = ? LIMIT 1', [mapId]);
                    if (mapRows.length > 0 && mapRows[0].name) {
                        mapName = mapRows[0].name;
                    }
                } catch (e) {}

                // Collect item IDs for batch dictionary lookup
                const item3Ids = new Set();
                const item4Ids = new Set();
                const item7Ids = new Set();

                const collectItem3 = (arr) => {
                    if (Array.isArray(arr)) {
                        arr.forEach(item => {
                            if (Array.isArray(item) && item.length > 0) {
                                item3Ids.add(parseInt(item[0]));
                            }
                        });
                    }
                };

                const collectItem47 = (arr) => {
                    if (Array.isArray(arr)) {
                        arr.forEach(item => {
                            if (Array.isArray(item) && item.length >= 2) {
                                const cat = parseInt(item[0]);
                                const id = parseInt(item[1]);
                                if (cat === 4) item4Ids.add(id);
                                else if (cat === 7) item7Ids.add(id);
                                else item4Ids.add(id);
                            }
                        });
                    }
                };

                collectItem3(itBodyRaw);
                collectItem3(bag3Raw);
                collectItem3(box3Raw);
                collectItem47(bag47Raw);
                collectItem47(box47Raw);

                // Batch query item3
                const item3Dict = {};
                if (item3Ids.size > 0) {
                    try {
                        const idsArray = Array.from(item3Ids);
                        const placeholders = idsArray.map(() => '?').join(',');
                        const [rows] = await db.execute(`SELECT * FROM item3 WHERE id IN (${placeholders})`, idsArray);
                        rows.forEach(r => { item3Dict[r.id] = r; });
                    } catch (e) {}
                }

                // Batch query item4
                const item4Dict = {};
                if (item4Ids.size > 0) {
                    try {
                        const idsArray = Array.from(item4Ids);
                        const placeholders = idsArray.map(() => '?').join(',');
                        const [rows] = await db.execute(`SELECT * FROM item4 WHERE id IN (${placeholders})`, idsArray);
                        rows.forEach(r => { item4Dict[r.id] = r; });
                    } catch (e) {}
                }

                // Batch query item7
                const item7Dict = {};
                if (item7Ids.size > 0) {
                    try {
                        const idsArray = Array.from(item7Ids);
                        const placeholders = idsArray.map(() => '?').join(',');
                        const [rows] = await db.execute(`SELECT * FROM item7 WHERE id IN (${placeholders})`, idsArray);
                        rows.forEach(r => { item7Dict[r.id] = r; });
                    } catch (e) {}
                }

                const CLAZZ_NAMES = { 1: 'Võ Sĩ', 2: 'Kiếm Khách', 3: 'Đầu Bếp', 4: 'Hoa Tiêu', 5: 'Xạ Thủ' };
                const TYPE_EQUIP_NAMES = {
                    0: 'Vũ khí', 1: 'Nón / Mũ', 2: 'Dây chuyền', 3: 'Áo', 4: 'Quần',
                    5: 'Găng tay / Nhẫn', 6: 'Trái tim / Trái Ác Quỷ', 7: 'Ốc sên / Pet',
                    8: 'Thắt lưng', 9: 'Giày', 10: 'Bảo vật'
                };
                const COLOR_NAMES = {
                    0: { text: 'Trắng', color: '#ffffff' },
                    1: { text: 'Xanh lá', color: '#2ecc71' },
                    2: { text: 'Xanh lam', color: '#3498db' },
                    3: { text: 'Tím', color: '#9b59b6' },
                    4: { text: 'Cam', color: '#e67e22' },
                    5: { text: 'Đỏ', color: '#e74c3c' },
                    8: { text: 'Thần thoại', color: '#ff3366' }
                };

                const formatItem3 = (raw) => {
                    if (!Array.isArray(raw) || raw.length === 0) return null;
                    const templateId = parseInt(raw[0]);
                    const levelup = parseInt(raw[1] || 0);
                    const typelock = parseInt(raw[2] || 0);
                    const isHoanMy = parseInt(raw[6] || 0);
                    const index = raw[12] !== undefined ? parseInt(raw[12]) : null;

                    const template = item3Dict[templateId] || null;
                    const name = template ? template.name : `Trang bị #${templateId}`;
                    const typeEquip = template ? template.typeequip : (index !== null ? index : 0);
                    const typeEquipName = TYPE_EQUIP_NAMES[typeEquip] || `Loại ${typeEquip}`;
                    const color = template ? template.color : 0;
                    const colorMeta = COLOR_NAMES[color] || { text: 'Thường', color: '#ffffff' };

                    // Parse options
                    let options = [];
                    try {
                        const op1 = typeof raw[8] === 'string' ? JSON.parse(raw[8]) : raw[8];
                        if (Array.isArray(op1)) {
                            op1.forEach(op => {
                                if (Array.isArray(op) && op.length >= 2) {
                                    options.push({ id: op[0], param: op[1] });
                                }
                            });
                        }
                    } catch (e) {}

                    return {
                        templateId,
                        name,
                        levelup,
                        typelock,
                        isHoanMy,
                        index,
                        typeEquip,
                        typeEquipName,
                        color,
                        colorMeta,
                        options,
                        icon: template ? template.icon : 0,
                        levelReq: template ? template.level : 1,
                        clazz: template ? template.clazz : 0
                    };
                };

                const formatItem47 = (raw) => {
                    if (!Array.isArray(raw) || raw.length < 3) return null;
                    const category = parseInt(raw[0]);
                    const id = parseInt(raw[1]);
                    const quant = parseInt(raw[2]);

                    let template = null;
                    let catName = 'Vật phẩm';
                    if (category === 4) {
                        template = item4Dict[id];
                        catName = 'Dược phẩm / Rương';
                    } else if (category === 7) {
                        template = item7Dict[id];
                        catName = 'Nguyên liệu / Đá';
                    } else {
                        template = item4Dict[id] || item7Dict[id];
                        catName = category === 5 ? 'Nhiệm vụ' : 'Khác';
                    }

                    const name = template ? template.name : `Vật phẩm #${id}`;
                    const icon = template ? template.icon : 0;

                    return {
                        category,
                        catName,
                        id,
                        name,
                        quant,
                        icon
                    };
                };

                // Spending Milestones (Tích tiêu Ruby) definition
                const SPENDING_MILESTONES_CONFIG = [
                    { id: 0, num: 500, label: '500 Ruby' },
                    { id: 1, num: 1000, label: '1.000 Ruby' },
                    { id: 2, num: 3000, label: '3.000 Ruby' },
                    { id: 3, num: 5000, label: '5.000 Ruby' },
                    { id: 4, num: 10000, label: '10.000 Ruby' },
                    { id: 5, num: 30000, label: '30.000 Ruby' },
                    { id: 6, num: 50000, label: '50.000 Ruby' },
                    { id: 7, num: 100000, label: '100.000 Ruby (Trùm Ve Chai)' }
                ];

                const claimedTieuStr = pl.claimed_tichtieu_ruby || '';
                const claimedTieuParts = claimedTieuStr.split(',').map(s => s.trim()).filter(Boolean);
                const claimedTieuSet = new Set(
                    claimedTieuParts.map(s => isNaN(s) ? s : Number(s))
                );
                const tichTieuCheckArr = safeJsonParse(pl.tich_tieu_check, []);
                const totalTieu = Math.max(parseInt(pl.tichtieu_ruby || 0), parseInt(pl.tieu_ruby || 0));

                const spendingMilestones = SPENDING_MILESTONES_CONFIG.map((m, idx) => {
                    const isClaimed = claimedTieuSet.has(idx) || claimedTieuSet.has(m.num) || (Array.isArray(tichTieuCheckArr) && tichTieuCheckArr[idx] === 1);
                    const isReached = totalTieu >= m.num;
                    return {
                        ...m,
                        isClaimed,
                        isReached,
                        canClaim: isReached && !isClaimed
                    };
                });

                playerData = {
                    id: pl.id,
                    name: pl.name,
                    clazz: pl.clazz,
                    clazzName: CLAZZ_NAMES[pl.clazz] || `Phái ${pl.clazz}`,
                    level: parseInt(levelArr[0] || 1),
                    exp: parseInt(levelArr[1] || 0),
                    thongthao: parseInt(levelArr[2] || 0),
                    
                    // Currency & Inven points
                    vang: parseInt(pointInvenArr[0] || 0), // Beri
                    ruby: parseInt(pointInvenArr[1] || 0), // Ruby / Kim cương
                    extol: parseInt(pointInvenArr[2] || 0), // Extol
                    vnd: parseInt(pointInvenArr[2] || 0), // Extol alias
                    bua: parseInt(pointInvenArr[3] || 0),
                    tichLuy: parseInt(pointInvenArr[4] || 0),
                    pvpWin: parseInt(pointInvenArr[5] || 0),
                    pvpLose: parseInt(pointInvenArr[6] || 0),
                    wantedPrice: parseInt(pointInvenArr[11] || 0),
                    
                    // Spending stats (Tích tiêu Ruby)
                    tichtieu_ruby: totalTieu,
                    tieu_ruby: parseInt(pl.tieu_ruby || 0),
                    claimed_tichtieu_ruby: claimedTieuStr,
                    spendingMilestones,
                    
                    // Stats
                    pvppoint: pl.pvppoint || 0,
                    wanted_point: pl.wanted_point || 0,
                    hangdong_stage: pl.hangdong_stage || 0,
                    lan_kills: pl.lan_kills || 0,
                    num_phao_hoa: pl.num_phao_hoa || 0,

                    // Location
                    location: {
                        mapId,
                        mapName,
                        zoneId: parseInt(siteArr[1] ?? 0),
                        hp: parseInt(siteArr[2] ?? 0),
                        mp: parseInt(siteArr[3] ?? 0),
                        x: parseInt(siteArr[4] ?? 0),
                        y: parseInt(siteArr[5] ?? 0)
                    },

                    // Potential stats
                    potential: {
                        pointsRemaining: parseInt(potentialArr[0] || 0),
                        sucManh: parseInt(potentialArr[1] || 1),
                        nhanhNhen: parseInt(potentialArr[2] || 1),
                        theLuc: parseInt(potentialArr[3] || 1),
                        tinhThan: parseInt(potentialArr[4] || 1),
                        phongThu: parseInt(potentialArr[5] || 1),
                        thongThaoPoints: parseInt(potentialArr[6] || 0)
                    },

                    // Equipments and inventory
                    equippedItems: itBodyRaw.map(formatItem3).filter(Boolean),
                    bagItems: bag3Raw.map(formatItem3).filter(Boolean),
                    bagSupplies: bag47Raw.map(formatItem47).filter(Boolean),
                    boxItems: box3Raw.map(formatItem3).filter(Boolean),
                    boxSupplies: box47Raw.map(formatItem47).filter(Boolean)
                };
            }
        }

        return res.json({
            success: true,
            account: accountData,
            player: playerData
        });
    } catch (err) {
        console.error('Admin get account detail error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/adjust_currency
router.post('/admin/adjust_currency', jwtRequired, isAdmin, async (req, res) => {
    const { username, ruby, vang, coin, tichnap, tongnap, vip, extol, vnd, tichtieu_ruby } = req.body;

    if (!username) {
        return res.json({ success: false, message: 'Thiếu tên tài khoản (username)!' });
    }

    try {
        const [accRows] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (accRows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        const acc = accRows[0];

        // Update accounts table (coin, tichnap, tongnap, sumamount, vip)
        const updateSets = [];
        const updateParams = [];

        let newCoin = coin !== undefined && coin !== null && coin !== '' ? parseInt(coin, 10) : null;
        let newTichNap = tichnap !== undefined && tichnap !== null && tichnap !== '' ? parseInt(tichnap, 10) : null;
        let newTongNap = tongnap !== undefined && tongnap !== null && tongnap !== '' ? parseInt(tongnap, 10) : null;
        let newVip = vip !== undefined && vip !== null && vip !== '' ? parseInt(vip, 10) : null;

        if (newCoin !== null && !isNaN(newCoin)) {
            updateSets.push('`coin` = ?');
            updateParams.push(Math.max(0, newCoin));
        }

        if (newTichNap !== null && !isNaN(newTichNap)) {
            updateSets.push('`tichnap` = ?');
            updateParams.push(Math.max(0, newTichNap));
        }

        if (newTongNap !== null && !isNaN(newTongNap)) {
            updateSets.push('`tongnap` = ?');
            updateParams.push(Math.max(0, newTongNap));
            updateSets.push('`sumamount` = ?');
            updateParams.push(Math.max(0, newTongNap));
        }

        if (newVip !== null && !isNaN(newVip)) {
            updateSets.push('`vip` = ?');
            updateParams.push(Math.max(0, Math.min(10, newVip)));
        } else if (newTongNap !== null && !isNaN(newTongNap)) {
            let autoVip = 0;
            if (newTongNap >= 10000000) autoVip = 7;
            else if (newTongNap >= 5000000) autoVip = 6;
            else if (newTongNap >= 3000000) autoVip = 5;
            else if (newTongNap >= 2000000) autoVip = 4;
            else if (newTongNap >= 1000000) autoVip = 3;
            else if (newTongNap >= 500000) autoVip = 2;
            else if (newTongNap >= 200000) autoVip = 1;

            updateSets.push('`vip` = ?');
            updateParams.push(autoVip);
        }

        if (updateSets.length > 0) {
            updateParams.push(username);
            await db.execute(`UPDATE accounts SET ${updateSets.join(', ')} WHERE user = ?`, updateParams);
        }

        // Update players table (vang, ruby, extol, tichtieu_ruby)
        let newRuby = ruby !== undefined && ruby !== null && ruby !== '' ? parseInt(ruby, 10) : null;
        let newVang = vang !== undefined && vang !== null && vang !== '' ? parseInt(vang, 10) : null;
        const rawExtol = extol !== undefined && extol !== null && extol !== '' ? extol : (vnd !== undefined && vnd !== null && vnd !== '' ? vnd : null);
        let newExtol = rawExtol !== null ? parseInt(rawExtol, 10) : null;
        let newTichTieu = tichtieu_ruby !== undefined && tichtieu_ruby !== null && tichtieu_ruby !== '' ? parseInt(tichtieu_ruby, 10) : null;

        if ((newRuby !== null && !isNaN(newRuby)) || (newVang !== null && !isNaN(newVang)) || (newExtol !== null && !isNaN(newExtol)) || (newTichTieu !== null && !isNaN(newTichTieu))) {
            let charName = null;
            if (acc.char) {
                try {
                    const chars = typeof acc.char === 'string' ? JSON.parse(acc.char) : acc.char;
                    if (Array.isArray(chars) && chars.length > 0) charName = chars[0];
                } catch (e) {}
            }

            if (charName) {
                const [plRows] = await db.execute('SELECT id, point_inven, tichtieu_ruby, tieu_ruby FROM players WHERE name = ? LIMIT 1', [charName]);
                if (plRows.length > 0) {
                    const pl = plRows[0];
                    let pointInvenArr = [];
                    try {
                        if (pl.point_inven) {
                            pointInvenArr = typeof pl.point_inven === 'string' ? JSON.parse(pl.point_inven) : pl.point_inven;
                        }
                    } catch (e) {}

                    if (!Array.isArray(pointInvenArr)) {
                        pointInvenArr = [0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0];
                    }
                    while (pointInvenArr.length < 12) {
                        pointInvenArr.push(0);
                    }

                    if (newVang !== null && !isNaN(newVang)) {
                        pointInvenArr[0] = Math.max(0, newVang);
                    }
                    if (newRuby !== null && !isNaN(newRuby)) {
                        pointInvenArr[1] = Math.max(0, newRuby);
                    }
                    if (newExtol !== null && !isNaN(newExtol)) {
                        pointInvenArr[2] = Math.max(0, newExtol);
                    }

                    const playerUpdates = ['`point_inven` = ?'];
                    const playerParams = [JSON.stringify(pointInvenArr)];

                    if (newTichTieu !== null && !isNaN(newTichTieu)) {
                        playerUpdates.push('`tichtieu_ruby` = ?', '`tieu_ruby` = ?');
                        playerParams.push(Math.max(0, newTichTieu), Math.max(0, newTichTieu));
                    }

                    playerParams.push(pl.id);
                    await db.execute(`UPDATE players SET ${playerUpdates.join(', ')} WHERE id = ?`, playerParams);
                }
            }
        }

        return res.json({ success: true, message: 'Cập nhật tiền tệ và tài sản thành công!' });
    } catch (err) {
        console.error('Admin adjust currency error:', err);
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
router.delete('/admin/giftcode/:id', jwtRequired, isAdmin, async (req, res) => {
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
        // 1. Fetch top 10 characters by level / exp (Tie-breaker: exp DESC, id ASC)
        const levelSql = `
            SELECT 
                name, 
                clazz,
                CASE 
                    WHEN level LIKE '[%]' THEN CAST(JSON_UNQUOTE(JSON_EXTRACT(level, '$[0]')) AS UNSIGNED)
                    WHEN level REGEXP '^[0-9]+$' THEN CAST(level AS UNSIGNED)
                    ELSE 1
                END as level,
                exp
            FROM players
            ORDER BY exp DESC, id ASC
            LIMIT 10
        `;
        const [levelRows] = await db.execute(levelSql);
        const topLevel = levelRows.map(row => ({
            name: row.name,
            level: parseInt(row.level || 0, 10),
            clazz: row.clazz !== null ? parseInt(row.clazz, 10) : 0
        }));

        // 2. Fetch top 10 characters by PvP point (Tie-breaker: pvppoint DESC, exp DESC, id ASC)
        const pvpSql = `
            SELECT name, pvppoint, clazz 
            FROM players 
            WHERE pvppoint > 0
            ORDER BY pvppoint DESC, exp DESC, id ASC 
            LIMIT 10
        `;
        const [pvpRows] = await db.execute(pvpSql);
        const topPvp = pvpRows.map(row => ({
            name: row.name,
            pvppoint: parseInt(row.pvppoint || 0, 10),
            clazz: row.clazz !== null ? parseInt(row.clazz, 10) : 0
        }));

        // 3. Fetch top 10 donators (Top Nạp) - Tạm thời đóng
        /*
        // Đồng bộ công thức real_amount với Game Server, tiêu chí bằng tiền thì ai nạp trước được xếp lên đầu
        const napSql = `
            SELECT 
                a.id,
                a.user, 
                a.\`char\`, 
                GREATEST(
                    COALESCE(a.sumamount, 0),
                    COALESCE(a.tichnap, 0),
                    CASE WHEN COALESCE(a.tongnap, 0) >= 2000000000 THEN a.tongnap - 2000000000 ELSE COALESCE(a.tongnap, 0) END,
                    COALESCE(a.vnd, 0)
                ) AS real_amount,
                COALESCE(
                    (SELECT MAX(rh.created_at) FROM recharge_history rh WHERE rh.username = a.user AND rh.status IN (1, 2)),
                    a.created_at,
                    '2099-12-31 23:59:59'
                ) AS last_recharge_time
            FROM accounts a
            WHERE a.sumamount > 0 
               OR a.tichnap > 0 
               OR (a.tongnap > 0 AND a.tongnap != 2000000000) 
               OR a.vnd > 0
            ORDER BY real_amount DESC, last_recharge_time ASC, a.id ASC
            LIMIT 10
        `;
        const [napRows] = await db.execute(napSql);
        const topNap = napRows.map(row => {
            let charName = null;
            try {
                const charArr = typeof row.char === 'string' ? JSON.parse(row.char) : row.char;
                if (Array.isArray(charArr) && charArr.length > 0) {
                    charName = charArr[0];
                }
            } catch (e) {
                // Ignore
            }
            
            const displayName = charName || row.user || 'Unknown';

            return {
                name: displayName,
                sumamount: parseInt(row.real_amount || 0, 10)
            };
        });
        */
        const topNap = [];

        // 4. Fetch top 10 clans (Top Clan - Tie-breaker: xp DESC, id ASC)
        const clanSql = `
            SELECT name, member, xp 
            FROM clan 
            ORDER BY xp DESC, id ASC 
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

// GET /api/recharge/bank_config (Lấy cấu hình ngân hàng công khai & sự kiện nạp)
router.get('/recharge/bank_config', async (req, res) => {
    let depositMultiplier = 1;
    let rechargeEnabled = true;
    try {
        const [rows] = await db.execute("SELECT `value` FROM `server_config` WHERE `key` = 'deposit_multiplier' LIMIT 1");
        if (rows.length > 0 && rows[0].value) {
            const parsed = parseInt(rows[0].value.trim(), 10);
            if (!isNaN(parsed) && parsed >= 1) {
                depositMultiplier = parsed;
            }
        }
        const [statusRows] = await db.execute("SELECT `value` FROM `server_config` WHERE `key` = 'recharge_enabled' LIMIT 1");
        if (statusRows.length > 0 && statusRows[0].value !== null) {
            rechargeEnabled = statusRows[0].value.trim() !== '0';
        }
    } catch (e) {
        console.error('Error fetching multiplier / recharge config in bank_config:', e.message);
    }

    return res.json({
        success: true,
        bankId: process.env.BANK_ID || 'MB',
        accountNo: process.env.BANK_ACCOUNT_NO || process.env.BANK_ACCOUNT || '123456789999',
        accountName: process.env.BANK_ACCOUNT_NAME || process.env.BANK_OWNER || 'NGUYEN VAN A',
        bankName: process.env.BANK_NAME || `${process.env.BANK_ID || 'MB'} Bank`,
        momoPhone: process.env.MOMO_PHONE || '0987654321',
        momoName: process.env.MOMO_NAME || 'NGUYEN VAN A',
        depositMultiplier: depositMultiplier,
        rechargeEnabled: rechargeEnabled
    });
});



// ==========================================
// GAME ITEMS & TEMPLATES MANAGEMENT (CRUD)
// ==========================================

const ALLOWED_ITEM_TABLES = {
    item4: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'icon', 'indexInfoPotion', 'price', 'priceruby', 'istrade', 'hpmpother', 'timedelay', 'value', 'timeactive', 'nameuse']
    },
    item3: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'clazz', 'typeequip', 'icon', 'level', 'color', 'typelock', 'numHoleDaDuc', 'chetac', 'ishoanmy', 'valuekichan', 'op_1', 'op_2', 'numlokham', 'mdakham', 'part', 'beri', 'ruby']
    },
    item7: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'type', 'icon', 'price', 'priceruby', 'istrade']
    },
    item4_info: {
        primaryKey: ['id'],
        searchColumns: ['info'],
        columns: ['id', 'info']
    },
    shoptichluy: {
        primaryKey: ['id', 'type'],
        searchColumns: ['info'],
        columns: ['id', 'type', 'point', 'info', 'limit', 'limit_data']
    },
    pet_template: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'icon', 'type', 'frame', 'op', 'show']
    },
    itemhair: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'icon', 'beri', 'ruby']
    },
    fashiontemplate: {
        primaryKey: ['id'],
        searchColumns: ['name', 'info'],
        columns: ['id', 'icon', 'name', 'info', 'mwear', 'op', 'price']
    },
    danhhieu: {
        primaryKey: ['id'],
        searchColumns: ['name'],
        columns: ['id', 'name', 'idicon', 'nframe', 'op', 'vnd', 'sell']
    }
};

// GET /api/admin/items/search
router.get('/admin/items/search', jwtRequired, isAdmin, async (req, res) => {
    try {
        const { table, keyword } = req.query;
        const page = parseInt(req.query.page) || 1;
        const limit = Math.min(Math.max(parseInt(req.query.limit) || 20, 1), 100);

        if (!table || !ALLOWED_ITEM_TABLES[table]) {
            return res.json({ success: false, message: 'Bảng dữ liệu không hợp lệ!' });
        }

        // Yêu cầu: Không hiển thị trước dữ liệu, chỉ tìm kiếm mới hiển thị
        if (!keyword || !keyword.trim()) {
            return res.json({
                success: true,
                table,
                data: [],
                total: 0,
                page: 1,
                limit,
                totalPages: 0
            });
        }

        const tableMeta = ALLOWED_ITEM_TABLES[table];
        const kw = keyword.trim();
        const whereClauses = [];
        const params = [];

        // Check if keyword is a valid integer (for exact ID search)
        if (/^\d+$/.test(kw)) {
            whereClauses.push('`id` = ?');
            params.push(parseInt(kw));
        }

        // Text search across designated search columns
        for (const col of tableMeta.searchColumns) {
            whereClauses.push(`\`${col}\` LIKE ?`);
            params.push(`%${kw}%`);
        }

        const whereSql = whereClauses.length > 0 ? `WHERE ${whereClauses.join(' OR ')}` : '';

        // Total count matching query
        const [[{ total }]] = await db.execute(
            `SELECT COUNT(*) as total FROM \`${table}\` ${whereSql}`,
            params
        );

        const totalPages = Math.ceil(total / limit);
        const currentPage = Math.min(Math.max(1, page), Math.max(1, totalPages));
        const offset = (currentPage - 1) * limit;

        // Query rows with pagination
        const [rows] = await db.execute(
            `SELECT * FROM \`${table}\` ${whereSql} ORDER BY \`id\` ASC LIMIT ${limit} OFFSET ${offset}`,
            params
        );

        return res.json({
            success: true,
            table,
            data: rows,
            total,
            page: currentPage,
            limit,
            totalPages
        });
    } catch (err) {
        console.error('Admin search items error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// POST /api/admin/items/create
router.post('/admin/items/create', jwtRequired, isAdmin, async (req, res) => {
    try {
        const { table, itemData } = req.body;

        if (!table || !ALLOWED_ITEM_TABLES[table]) {
            return res.json({ success: false, message: 'Bảng dữ liệu không hợp lệ!' });
        }

        if (!itemData || typeof itemData !== 'object') {
            return res.json({ success: false, message: 'Dữ liệu vật phẩm không hợp lệ!' });
        }

        const tableMeta = ALLOWED_ITEM_TABLES[table];
        const insertKeys = [];
        const insertValues = [];
        const placeholders = [];

        for (const col of tableMeta.columns) {
            if (itemData[col] !== undefined && itemData[col] !== null) {
                insertKeys.push(`\`${col}\``);
                insertValues.push(itemData[col]);
                placeholders.push('?');
            }
        }

        if (insertKeys.length === 0) {
            return res.json({ success: false, message: 'Không có dữ liệu hợp lệ để thêm!' });
        }

        const query = `INSERT INTO \`${table}\` (${insertKeys.join(', ')}) VALUES (${placeholders.join(', ')})`;
        await db.execute(query, insertValues);

        return res.json({ success: true, message: `Thêm bản ghi vào ${table} thành công!` });
    } catch (err) {
        console.error('Admin create item error:', err);
        return res.json({ success: false, message: `Lỗi thêm vật phẩm: ${err.message}` });
    }
});

// POST /api/admin/items/update
router.post('/admin/items/update', jwtRequired, isAdmin, async (req, res) => {
    try {
        const { table, itemData, originalKey } = req.body;

        if (!table || !ALLOWED_ITEM_TABLES[table]) {
            return res.json({ success: false, message: 'Bảng dữ liệu không hợp lệ!' });
        }

        if (!itemData || typeof itemData !== 'object' || !originalKey) {
            return res.json({ success: false, message: 'Dữ liệu cập nhật không hợp lệ!' });
        }

        const tableMeta = ALLOWED_ITEM_TABLES[table];
        const updateSets = [];
        const updateParams = [];

        for (const col of tableMeta.columns) {
            if (itemData[col] !== undefined) {
                updateSets.push(`\`${col}\` = ?`);
                updateParams.push(itemData[col]);
            }
        }

        if (updateSets.length === 0) {
            return res.json({ success: false, message: 'Không có trường nào cần cập nhật!' });
        }

        // Build WHERE clause based on original primary key(s)
        const whereClauses = [];
        for (const pkCol of tableMeta.primaryKey) {
            if (originalKey[pkCol] === undefined || originalKey[pkCol] === null) {
                return res.json({ success: false, message: `Thiếu khóa chính ${pkCol} để cập nhật!` });
            }
            whereClauses.push(`\`${pkCol}\` = ?`);
            updateParams.push(originalKey[pkCol]);
        }

        const query = `UPDATE \`${table}\` SET ${updateSets.join(', ')} WHERE ${whereClauses.join(' AND ')}`;
        await db.execute(query, updateParams);

        return res.json({ success: true, message: `Cập nhật bản ghi trong ${table} thành công!` });
    } catch (err) {
        console.error('Admin update item error:', err);
        return res.json({ success: false, message: `Lỗi cập nhật vật phẩm: ${err.message}` });
    }
});

// POST /api/admin/items/delete
router.post('/admin/items/delete', jwtRequired, isAdmin, async (req, res) => {
    try {
        const { table, key } = req.body;

        if (!table || !ALLOWED_ITEM_TABLES[table]) {
            return res.json({ success: false, message: 'Bảng dữ liệu không hợp lệ!' });
        }

        if (!key || typeof key !== 'object') {
            return res.json({ success: false, message: 'Thiếu thông tin khóa chính để xóa!' });
        }

        const tableMeta = ALLOWED_ITEM_TABLES[table];
        const whereClauses = [];
        const deleteParams = [];

        for (const pkCol of tableMeta.primaryKey) {
            if (key[pkCol] === undefined || key[pkCol] === null) {
                return res.json({ success: false, message: `Thiếu khóa chính ${pkCol} để xóa!` });
            }
            whereClauses.push(`\`${pkCol}\` = ?`);
            deleteParams.push(key[pkCol]);
        }

        const query = `DELETE FROM \`${table}\` WHERE ${whereClauses.join(' AND ')}`;
        await db.execute(query, deleteParams);

        return res.json({ success: true, message: `Đã xóa bản ghi khỏi ${table} thành công!` });
    } catch (err) {
        console.error('Admin delete item error:', err);
        return res.json({ success: false, message: `Lỗi xóa vật phẩm: ${err.message}` });
    }
});
// GET /api/admin/player-logs
router.get('/admin/player-logs', jwtRequired, isAdmin, async (req, res) => {
    try {
        const { startDate, endDate, playerName, type, page = 1, limit = 50 } = req.query;
        let query = 'SELECT * FROM player_logs WHERE 1=1';
        let countQuery = 'SELECT COUNT(*) as total FROM player_logs WHERE 1=1';
        let params = [];

        if (startDate) {
            query += ' AND created_at >= ?';
            countQuery += ' AND created_at >= ?';
            params.push(`${startDate} 00:00:00`);
        }
        if (endDate) {
            query += ' AND created_at <= ?';
            countQuery += ' AND created_at <= ?';
            params.push(`${endDate} 23:59:59`);
        }
        if (playerName) {
            query += ' AND player_name LIKE ?';
            countQuery += ' AND player_name LIKE ?';
            params.push(`%${playerName}%`);
        }
        if (type) {
            query += ' AND type = ?';
            countQuery += ' AND type = ?';
            params.push(type);
        } else {
            query += " AND type != 'drop_pick'";
            countQuery += " AND type != 'drop_pick'";
        }

        const safeLimit = Math.max(1, Math.min(200, parseInt(limit, 10) || 50));
        const safePage = Math.max(1, parseInt(page, 10) || 1);
        const offset = (safePage - 1) * safeLimit;

        query += ` ORDER BY created_at DESC LIMIT ${safeLimit} OFFSET ${offset}`;

        const [rows] = await db.query(query, params);
        const [countRows] = await db.query(countQuery, params);
        
        const total = countRows && countRows[0] ? countRows[0].total : 0;

        return res.json({
            success: true,
            data: rows,
            total: total,
            page: safePage,
            totalPages: Math.ceil(total / safeLimit)
        });
    } catch (err) {
        console.error('Admin get player logs error:', err);
        return res.json({ success: false, message: `Lỗi lấy lịch sử: ${err.message}` });
    }
});

module.exports = router;
