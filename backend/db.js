const { DataSource } = require("typeorm");
const fs = require("fs");
require("dotenv").config();

const AppDataSource = new DataSource({
    type: "postgres",
    host: process.env.DB_HOST,
    port: process.env.DB_PORT,
    username: process.env.DB_USER,
    password: process.env.DB_PASSWORD,
    database: process.env.DB_NAME,
    ssl: {
        rejectUnauthorized: true,
        ca: fs.readFileSync("./ca.pem").toString(),
    },
    entities: [require("./src/files/entity"), require('./src/users/entity')], // Include the Files entity
    synchronize: true, // Auto-sync database (disable in production)
    logging: true,
});

module.exports = { AppDataSource };
