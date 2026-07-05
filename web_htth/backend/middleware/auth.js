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
    const userId = req.jwt_user_id;
    if (!userId) {
        return res.json({ success: false, message: 'Không có quyền' });
    }

    try {
        const [rows] = await db.execute('SELECT * FROM accounts WHERE id = ?', [userId]);
        if (rows.length === 0) {
            return res.json({ success: false, message: 'Không có quyền' });
        }

        const user = rows[0];
        if (user.user !== 'admin') {
            return res.json({ success: false, message: 'Không có quyền' });
        }

        req.adminUser = user;
        next();
    } catch (err) {
        return res.json({ success: false, message: 'Không có quyền' });
    }
};

module.exports = {
    jwtRequired,
    isAdmin
};
