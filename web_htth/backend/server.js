const express = require('express');
const cors = require('cors');
require('dotenv').config();

const apiRouter = require('./routes/api');

const app = express();
const PORT = process.env.PORT || 8000;

// CORS setup
const corsOptions = {
    origin: ['http://localhost:5173', 'http://127.0.0.1:5173'],
    credentials: true,
};
app.use(cors(corsOptions));

// Parsing body middleware
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

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
app.listen(PORT, () => {
    console.log(`Server is running on port ${PORT}`);
});
