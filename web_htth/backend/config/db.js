const mysql = require('mysql2/promise');
require('dotenv').config();

const pool = mysql.createPool({
    host: process.env.DB_HOST || 'localhost',
    user: process.env.DB_USER || 'root',
    password: process.env.DB_PASSWORD !== undefined ? process.env.DB_PASSWORD : '123456',
    database: process.env.DB_NAME || 'htth',
    port: parseInt(process.env.DB_PORT || '3306'),
    waitForConnections: true,
    connectionLimit: 10,
    queueLimit: 0
});

// Simple connectivity check
pool.getConnection()
    .then(connection => {
        console.log('Database connected successfully.');
        connection.release();
    })
    .catch(err => {
        console.error('Database connection failed:', err.message);
    });

module.exports = pool;
