const jwt = require('jsonwebtoken');
const db = require('../config/db');
require('dotenv').config();

const JWT_SECRET = process.env.JWT_SECRET || 'default_jwt_secret_key_for_development_only';

const jwtRequired = (req, res, next) => {
    const authHeader = req.headers['authorization'];
    if (!authHeader) {
        return res.status(401).json({ success: false, message: 'Thiếu token xác thực' });
    }

    const parts = authHeader.split(' ');
    if (parts.length !== 2 || parts[0].toLowerCase() !== 'bearer') {
        return res.status(401).json({ success: false, message: 'Token không đúng định dạng Bearer' });
    }

    const token = parts[1];
    try {
        const decoded = jwt.verify(token, JWT_SECRET);
        req.jwt_user_id = decoded.user_id;
        next();
    } catch (err) {
        return res.status(401).json({ success: false, message: 'Token không hợp lệ hoặc đã hết hạn' });
    }
};

const isAdmin = async (req, res, next) => {
    let userId = req.jwt_user_id;
    if (!userId) {
        // Fallback: extract token if jwtRequired was not placed before isAdmin in route
        const authHeader = req.headers['authorization'];
        if (authHeader) {
            const parts = authHeader.split(' ');
            if (parts.length === 2 && parts[0].toLowerCase() === 'bearer') {
                try {
                    const decoded = jwt.verify(parts[1], JWT_SECRET);
                    userId = decoded.user_id;
                    req.jwt_user_id = userId;
                } catch (e) {}
            }
        }
    }

    if (!userId) {
        return res.status(401).json({ success: false, message: 'Không có quyền truy cập (Thiếu xác thực)' });
    }

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE id = ?', [userId]);
        if (rows.length === 0) {
            return res.status(403).json({ success: false, message: 'Không có quyền truy cập (Tài khoản không tồn tại)' });
        }

        const user = rows[0];
        const isAuthorized = user.user === 'admin' || user.admin === 1 || user.admin === '1';
        if (!isAuthorized) {
            return res.status(403).json({ success: false, message: 'Không có quyền truy cập quản trị' });
        }

        req.adminUser = user;
        next();
    } catch (err) {
        console.error('isAdmin middleware error:', err);
        return res.status(500).json({ success: false, message: 'Không có quyền truy cập (Lỗi hệ thống)' });
    }
};

module.exports = {
    jwtRequired,
    isAdmin
};
