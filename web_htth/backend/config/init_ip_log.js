const db = require('./db');

async function initIpLogTable() {
    try {
        const createTableSql = `
            CREATE TABLE IF NOT EXISTS ip_register_logs (
                id INT AUTO_INCREMENT PRIMARY KEY,
                ip VARCHAR(100) NOT NULL,
                created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
                KEY idx_ip_created_at (ip, created_at)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
        `;
        await db.query(createTableSql);
        console.log("Database table 'ip_register_logs' checked/created successfully.");
    } catch (err) {
        console.error("Error initializing ip_register_logs table:", err.message);
    }
}

module.exports = initIpLogTable;
