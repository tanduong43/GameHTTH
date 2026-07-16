const mysql = require('mysql2/promise');

async function main() {
  const connection = await mysql.createConnection({
    host: 'localhost',
    user: 'root',
    password: '123456',
    database: 'htth'
  });
  
  try {
    await connection.query('ALTER TABLE players ADD COLUMN hangdong_stage INT DEFAULT 0;');
    console.log("Column hangdong_stage added successfully!");
  } catch(e) {
    console.log("Error:", e.message);
  }
  await connection.end();
}

main();
