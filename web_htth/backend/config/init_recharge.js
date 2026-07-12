const db = require('./db');
const initTransactionsTable = require('./init_transactions');

async function initRechargeTable() {
    // Initialize transactions table
    await initTransactionsTable();

    try {
        // Try selecting one of the newer columns
        let needsRecreate = false;
        try {
            await db.query("SELECT request_id FROM recharge_history LIMIT 1");
        } catch (err) {
            // Column does not exist or table does not exist
            needsRecreate = true;
        }

        if (needsRecreate) {
            console.log("Recharge history table needs initialization/migration. Checking & re-creating table...");
            // Drop existing table if empty/invalid to prevent schema conflicts
            let rowCount = 0;
            try {
                const [countRows] = await db.query("SELECT COUNT(*) AS count FROM recharge_history");
                rowCount = countRows[0].count;
            } catch (err) {
                // Table doesn't exist
            }

            if (rowCount === 0) {
                await db.query("DROP TABLE IF EXISTS recharge_history");
                const createTableSql = `
                    CREATE TABLE recharge_history (
                        id INT AUTO_INCREMENT PRIMARY KEY,
                        username VARCHAR(255) NOT NULL,
                        amount INT NOT NULL DEFAULT 0,
                        real_amount INT NOT NULL DEFAULT 0,
                        type VARCHAR(50) DEFAULT 'card',
                        status TINYINT NOT NULL DEFAULT 0, -- 0: pending, 1: success, 2: success wrong amount, 3: failed
                        request_id VARCHAR(255) DEFAULT NULL,
                        telco VARCHAR(50) DEFAULT NULL,
                        serial VARCHAR(255) DEFAULT NULL,
                        code VARCHAR(255) DEFAULT NULL,
                        description VARCHAR(255) DEFAULT NULL,
                        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
                        INDEX idx_username (username),
                        INDEX idx_request_id (request_id)
                    ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
                `;
                await db.query(createTableSql);
                console.log("Database table 'recharge_history' initialized successfully.");
            } else {
                console.log("Table 'recharge_history' has data but columns are missing. Migrating columns dynamically...");
                // Alter table dynamically to add missing columns without dropping data
                const colsToAdd = [
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS real_amount INT NOT NULL DEFAULT 0 AFTER amount",
                    "ALTER TABLE recharge_history MODIFY COLUMN type VARCHAR(50) DEFAULT 'card'",
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS status TINYINT NOT NULL DEFAULT 0 AFTER type",
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS request_id VARCHAR(255) DEFAULT NULL AFTER status",
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS telco VARCHAR(50) DEFAULT NULL AFTER request_id",
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS serial VARCHAR(255) DEFAULT NULL AFTER telco",
                    "ALTER TABLE recharge_history ADD COLUMN IF NOT EXISTS code VARCHAR(255) DEFAULT NULL AFTER serial",
                    "ALTER TABLE recharge_history ADD INDEX IF NOT EXISTS idx_username (username)",
                    "ALTER TABLE recharge_history ADD INDEX IF NOT EXISTS idx_request_id (request_id)"
                ];
                for (const query of colsToAdd) {
                    try {
                        await db.query(query);
                    } catch (alterErr) {
                        console.warn("Failed dynamic alter column: ", alterErr.message);
                    }
                }
                console.log("Database table 'recharge_history' columns migrated.");
            }
        } else {
            console.log("Database table 'recharge_history' is valid.");
        }
    } catch (err) {
        console.error("Error initializing recharge_history table:", err.message);
    }
}

module.exports = initRechargeTable;
