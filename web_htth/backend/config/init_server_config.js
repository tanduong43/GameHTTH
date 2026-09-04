const db = require('./db');

async function initServerConfigTable() {
    try {
        const createTableSql = `
            CREATE TABLE IF NOT EXISTS server_config (
                \`key\` VARCHAR(100) NOT NULL PRIMARY KEY,
                \`value\` TEXT NOT NULL,
                \`description\` VARCHAR(255) DEFAULT NULL,
                \`updated_at\` TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
            ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
        `;
        await db.query(createTableSql);

        // Ensure default key for deposit_multiplier exists
        const insertDefaultSql = `
            INSERT INTO server_config (\`key\`, \`value\`, \`description\`)
            VALUES ('deposit_multiplier', '1', 'Hệ số nhân nạp thẻ / ngân hàng (1 = bình thường, 2 = x2, 3 = x3)')
            ON DUPLICATE KEY UPDATE \`description\` = VALUES(\`description\`);
        `;
        await db.query(insertDefaultSql);

        console.log("Database table 'server_config' initialized successfully.");
    } catch (err) {
        console.error("Error initializing server_config table:", err.message);
    }
}

module.exports = initServerConfigTable;
