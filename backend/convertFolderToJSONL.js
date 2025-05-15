const fs = require('fs');
const path = require('path');
const csv = require('csv-parser');

const inputFolder = path.join(__dirname, 'Evnat');

fs.readdir(inputFolder, (err, files) => {
    if (err) {
        return console.error("Error reading folder:", err);
    }

    files.filter(file => file.endsWith('.csv')).forEach(file => {
        const inputFilePath = path.join(inputFolder, file);
        const outputFileName = file.replace('.csv', '.jsonl');
        const outputFilePath = path.join(inputFolder, outputFileName); 
        const outputStream = fs.createWriteStream(outputFilePath, { flags: 'w' });

        fs.createReadStream(inputFilePath)
            .pipe(csv())
            .on('data', (row) => {
                outputStream.write(JSON.stringify(row) + '\n');
            })
            .on('end', () => {
                outputStream.end();
                console.log(`Converted: ${file} → ${outputFileName}`);
            })
            .on('error', (err) => {
                console.error(`Error processing ${file}:`, err);
            });
    });
});
