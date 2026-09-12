const db = require('./db');

async function initAccountsTable() {
    try {
        const requiredDefaults = [
            'vip', 'vnd', 'phone', 'activated', 'kh', 'mcs', 
            'gioithieu', 'admin', 'active', 'tichdiem'
        ];
        
        for (const col of requiredDefaults) {
            try {
                await db.query(`ALTER TABLE accounts ALTER COLUMN \`${col}\` SET DEFAULT 0`);
            } catch (err) {
                // Column might not exist or already set
            }
        }

        // Add last_login column if not present
        try {
            const [cols] = await db.query("SHOW COLUMNS FROM accounts LIKE 'last_login'");
            if (cols.length === 0) {
                await db.query("ALTER TABLE accounts ADD COLUMN `last_login` DATETIME NULL DEFAULT CURRENT_TIMESTAMP");
                console.log("Column 'last_login' added to accounts table.");
            }
        } catch (errCol) {
            console.error("Error checking/adding 'last_login' column:", errCol.message);
        }

        // Backfill last_login from players.date or created_at for any null entries
        try {
            await db.query(`
                UPDATE accounts a
                LEFT JOIN (
                    SELECT a2.id, MAX(STR_TO_DATE(LEFT(p.date, 19), '%Y-%m-%dT%H:%i:%s')) as latest_pdate
                    FROM accounts a2
                    JOIN players p ON (JSON_VALID(a2.char) AND JSON_CONTAINS(a2.char, JSON_QUOTE(p.name)))
                    GROUP BY a2.id
                ) p_sub ON a.id = p_sub.id
                SET a.last_login = COALESCE(p_sub.latest_pdate, a.created_at, NOW())
                WHERE a.last_login IS NULL
            `);
        } catch (errBackfill) {
            console.error("Error backfilling last_login:", errBackfill.message);
        }

        console.log("Database table 'accounts' column defaults checked/updated successfully.");
    } catch (err) {
        console.error("Error initializing accounts table defaults:", err.message);
    }
}

module.exports = initAccountsTable;
