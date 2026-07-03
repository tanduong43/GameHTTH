const express = require('express');
const router = express.Router();
const jwt = require('jsonwebtoken');
const db = require('../config/db');
const { jwtRequired, isAdmin } = require('../middleware/auth');
require('dotenv').config();

const JWT_SECRET = process.env.JWT_SECRET || 'django-insecure-$(7p(&$6!)#@@*-p=*yiyte@o=%7ug%uleploraez8885&q-)1';

// POST /api/register/
router.post('/register', async (req, res) => {
    const { username, password } = req.body;

    if (!username || !password || username.length < 3 || password.length < 3) {
        return res.json({ success: false, message: 'Tài khoản và mật khẩu phải từ 3 ký tự trở lên!' });
    }

    try {
        const [existing] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (existing.length > 0) {
            return res.json({ success: false, message: 'Tài khoản đã tồn tại!' });
        }

        await db.execute(
            'INSERT INTO accounts (user, `pass`, char, onl, `lock`, status, coin, vip) VALUES (?, ?, ?, ?, ?, ?, ?, ?)',
            [username, password, '[]', 0, 0, 0, 0, 0]
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
        const token = jwt.sign({ user_id: account.id }, JWT_SECRET, { expiresIn: '7d' });

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

        await db.execute('UPDATE accounts SET status = 1 WHERE id = ?', [req.jwt_user_id]);
        return res.json({ success: true, message: 'Kích hoạt thành công!' });
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

// POST /api/admin/add_coin/
router.post('/admin/add_coin', jwtRequired, isAdmin, async (req, res) => {
    const { username, amount } = req.body;
    const coinAmount = parseInt(amount || 0, 10);

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE user = ?', [username]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không tìm thấy tài khoản!' });
        }

        await db.execute('UPDATE accounts SET coin = coin + ? WHERE user = ?', [coinAmount, username]);
        return res.json({ success: true, message: `Đã cộng ${coinAmount} Coin cho ${username}!` });
    } catch (err) {
        console.error('Admin add coin error:', err);
        return res.json({ success: false, message: `Lỗi hệ thống: ${err.message}` });
    }
});

// GET /api/admin/accounts/
router.get('/admin/accounts', jwtRequired, isAdmin, async (req, res) => {
    try {
        const [rows] = await db.execute('SELECT id, user, coin, status, `lock` FROM accounts ORDER BY id DESC');
        return res.json({ success: true, accounts: rows });
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
            await db.execute('UPDATE accounts SET status = 1 WHERE user = ?', [username]);
            return res.json({ success: true, message: 'Đã kích hoạt thành viên!' });
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
    const { code, beri, ruby, gioihan, item, thongbao, luotnhap, used, special } = req.body;

    const beriInt = parseInt(beri || 0, 10);
    const rubyInt = parseInt(ruby || 0, 10);
    const gioihanInt = parseInt(gioihan ?? 1, 10);
    const luotnhapInt = parseInt(luotnhap ?? 0, 10);
    const itemJson = item ?? '[]';
    const thongbaoStr = thongbao ?? '';
    const usedStr = used ?? '';
    const specialStr = special ?? '';

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
            'INSERT INTO giftcode (giftname, beri, ruby, item, thongbao, luotnhap, gioihan, used, special) VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?)',
            [code, beriInt, rubyInt, itemJson, thongbaoStr, luotnhapInt, gioihanInt, usedStr, specialStr]
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
    const { code, beri, ruby, gioihan, item, thongbao, luotnhap, used, special } = req.body;

    const beriInt = parseInt(beri || 0, 10);
    const rubyInt = parseInt(ruby || 0, 10);
    const gioihanInt = parseInt(gioihan ?? 1, 10);
    const luotnhapInt = parseInt(luotnhap ?? 0, 10);
    const itemJson = item ?? '[]';
    const thongbaoStr = thongbao ?? '';
    const usedStr = used ?? '';
    const specialStr = special ?? '';

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
            'UPDATE giftcode SET giftname = ?, beri = ?, ruby = ?, item = ?, thongbao = ?, luotnhap = ?, gioihan = ?, used = ?, special = ? WHERE id = ?',
            [code, beriInt, rubyInt, itemJson, thongbaoStr, luotnhapInt, gioihanInt, usedStr, specialStr, id]
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

module.exports = router;

