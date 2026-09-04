const express = require('express');
const cors = require('cors');
const path = require('path');
const http = require('http');
const { Server } = require('socket.io');
require('dotenv').config();

const apiRouter = require('./routes/api');
const initNewsTable = require('./config/init_news');
const initIpLogTable = require('./config/init_ip_log');
const initRechargeTable = require('./config/init_recharge');
const initAccountsTable = require('./config/init_accounts');
const initServerConfigTable = require('./config/init_server_config');

// Initialize database tables, seed data, and schema defaults
initNewsTable();
initIpLogTable();
initRechargeTable();
initAccountsTable();
initServerConfigTable();

const app = express();
const PORT = process.env.PORT || 8000;

// CORS setup
const corsOptions = {
    origin: function (origin, callback) {
        // Allow requests with no origin (like mobile apps, curl, postman)
        if (!origin) return callback(null, true);
        // Allow any origin during local development
        return callback(null, true);
    },
    credentials: true,
};
app.use(cors(corsOptions));

// Parsing body middleware with limits
app.use(express.json({ limit: '10mb' }));
app.use(express.urlencoded({ limit: '10mb', extended: true }));

// Serve uploads statically
app.use('/uploads', express.static(path.join(__dirname, 'uploads')));

// Log requests middleware (useful for debugging)
app.use((req, res, next) => {
    console.log(`[${new Date().toISOString()}] ${req.method} ${req.url}`);
    next();
});

// Root API path
app.use('/api', apiRouter);

// Health check endpoint
app.get('/', (req, res) => {
    res.json({ status: 'ok', message: 'Node.js backend is running.' });
});

// Handling 404
app.use((req, res) => {
    res.status(404).json({ success: false, message: 'Route not found' });
});

// Start the server
const server = http.createServer(app);
const io = new Server(server, {
    cors: corsOptions
});
app.set('io', io);

io.on('connection', (socket) => {
    console.log(`Socket connected: ${socket.id}`);
    socket.on('join', (data) => {
        if (data && data.username) {
            socket.join(`user_${data.username}`);
            console.log(`User ${data.username} joined room user_${data.username}`);
        }
    });
    socket.on('disconnect', () => {
        console.log(`Socket disconnected: ${socket.id}`);
    });
});

server.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
