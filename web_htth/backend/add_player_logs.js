const mysql = require('mysql2/promise');

async function main() {
  const connection = await mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '123456',
    database: 'full_db_htth'
  });
  
  try {
    const query = `
      CREATE TABLE IF NOT EXISTS \`player_logs\` (
        \`id\` int(11) NOT NULL AUTO_INCREMENT,
        \`player_name\` varchar(50) NOT NULL,
        \`type\` varchar(20) NOT NULL COMMENT 'Loại: ruby, item, buff',
        \`action\` varchar(255) NOT NULL,
        \`created_at\` timestamp NOT NULL DEFAULT current_timestamp(),
        PRIMARY KEY (\`id\`),
        INDEX \`idx_player_name\` (\`player_name\`),
        INDEX \`idx_created_at\` (\`created_at\`)
      ) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
    `;
    await connection.query(query);
    console.log("Table player_logs created successfully in full_db_htth!");
  } catch(e) {
    console.log("Error:", e.message);
  }
  await connection.end();
}

main();
