require("dotenv").config();
const admin = require("firebase-admin");
const fs = require("fs");

const serviceAccountPath = process.env.FIREBASE_CREDENTIALS;
const serviceAccount = JSON.parse(fs.readFileSync(serviceAccountPath, "utf8"));

admin.initializeApp({
    credential: admin.credential.cert(serviceAccount),
});

module.exports = admin;
