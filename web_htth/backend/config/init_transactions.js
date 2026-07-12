const db = require('./db');

async function initTransactionsTable() {
    try {
        const createTableSql = `
            CREATE TABLE IF NOT EXISTS transactions (
                id INT AUTO_INCREMENT PRIMARY KEY,
                username VARCHAR(255) NOT NULL,
                type VARCHAR(50) NOT NULL, -- 'deposit', 'admin_add', 'activate', etc.
                amount INT NOT NULL,
                balance_before INT NOT NULL,
                balance_after INT NOT NULL,
                description VARCHAR(255) DEFAULT NULL,
                created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                INDEX idx_username (username)
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
        `;
        await db.query(createTableSql);
        console.log("Database table 'transactions' initialized successfully.");
    } catch (err) {
        console.error("Error initializing transactions table:", err.message);
    }
}

module.exports = initTransactionsTable;
