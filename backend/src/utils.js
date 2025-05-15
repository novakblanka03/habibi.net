const path = require('path');
const fs = require('fs-extra');
const AdmZip = require('adm-zip');
const fetch = require('node-fetch');
const admin = require("../firebase-config");

const sanitizeFolderName = name => name
    .trim()
    .toLowerCase()
    .normalize('NFD')
    .replace(/\p{Diacritic}/gu, '') // Remove accents
    .replace(/[^a-z0-9\s]/g, '') // Remove special characters
    .replace(/\s+/g, '_'); // Replace spaces with underscores

const extractZip = async (filePath, outputDir) => {
    const zip = new AdmZip(filePath);
    zip.getEntries()
        .filter(entry => entry.entryName.endsWith('.pdf'))
        .forEach(entry => {
            const outputPath = path.join(outputDir, entry.entryName);
            console.log(`Extracting PDF: ${entry.entryName} to ${outputPath}`);
            zip.extractEntryTo(entry, outputDir, false, true);
        });
};

async function downloadFile(url, downloadPath) {
    const response = await fetch(url);
    if (!response.ok) throw new Error(`Failed to fetch ${url}: ${response.statusText}`);

    const fileStream = fs.createWriteStream(downloadPath);

    return new Promise((resolve, reject) => {
        // If the response body is a stream, use pipe
        if (response.body && response.body.pipe) {
            response.body.pipe(fileStream);
            response.body.on('error', reject);
            fileStream.on('finish', resolve);
        } else {
            // If it's not a stream, assume it's a buffer and write it directly
            response.buffer()
                .then((data) => {
                    fileStream.write(data, (err) => {
                        if (err) reject(err);
                        else resolve();
                    });
                })
                .catch(reject);
        }
    });
}

async function getUser(uid) {
    try {
        const user = await admin.auth().getUser(uid);
        console.log("User data:", user.toJSON());
    } catch (error) {
        console.error("Error fetching user:", error);
    }
}

module.exports = {
    sanitizeFolderName,
    extractZip,
    downloadFile,
    getUser,
}
