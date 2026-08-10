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
        console.log("Database table 'accounts' column defaults checked/updated successfully.");
    } catch (err) {
        console.error("Error initializing accounts table defaults:", err.message);
    }
}

module.exports = initAccountsTable;
