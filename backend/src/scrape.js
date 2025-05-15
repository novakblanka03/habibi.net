const puppeteer = require('puppeteer');
const path = require('path');
const fs = require('fs-extra');
const {uploadFile, isFileUploaded} = require('./files/controller.js');
const {sanitizeFolderName, extractZip, downloadFile} = require('./utils');
const {updateVariantaBarem} = require("./files/controller");

const scrapeYear = async (year, exam) => {
    let baseURL;
    if (exam === "EN") {
        baseURL = `http://subiecte${year === new Date().getFullYear() + 1 ? '' : year}.edu.ro/${year}/evaluarenationala/modeledesubiecte/`;
    } else {
        baseURL = `http://subiecte${year === new Date().getFullYear() + 1 ? '' : year}.edu.ro/${year}/bacalaureat/modeledesubiecte/probescrise/`;
    }
    console.log(`Scraping year ${year}: ${baseURL}`);

    const browser = await puppeteer.launch({
        headless: true,
    });
    const page = await browser.newPage();

    try {
        await page.goto(baseURL, {waitUntil: 'domcontentloaded'});

        const links = await page.evaluate(() => {
            const linkElements = Array.from(document.querySelectorAll('a[href$=".zip"]'));
            return linkElements.map(link => ({
                href: link.href,
                name: link.textContent.trim() || link.href.split('/').pop(),
            }));
        });

        console.log(`Found ${links.length} ZIP files for ${year}.`);

        const __dirname = path.dirname(__filename);
        const yearDir = path.join(__dirname, `downloads${exam}`, String(year));
        await fs.ensureDir(yearDir);

        for (const link of links) {
            console.log(`Processing ZIP: ${link.name}`);
            const sanitizedFolderName = sanitizeFolderName(link.name.replace('.zip', ''));
            const zipFolderPath = path.join(yearDir, sanitizedFolderName);
            await fs.ensureDir(zipFolderPath);

            const zipPath = path.join(yearDir, `${sanitizedFolderName}.zip`);
            console.log(`Downloading ZIP: ${link.href}`);
            await downloadFile(link.href, zipPath);

            console.log(`Extracting ZIP: ${zipPath}`);
            await extractZip(zipPath, zipFolderPath);

            const pdfFiles = await fs.readdir(zipFolderPath);

            console.log('PDF files:', pdfFiles);

            const uploadedFiles = {};
            for (const pdf of pdfFiles) {
                console.log(`Processing PDF: ${pdf}`);
                try {
                    const pdfPath = path.join(zipFolderPath, pdf);
                    const examType = exam;
                    const docType = pdf.includes('var') ? 'varianta' : 'barem';

                    const isUploaded = await isFileUploaded(pdf);
                    if (!isUploaded) {
                        console.log(`Uploading new file: ${pdf}`);
                        const fileData = {
                            name: pdf,
                            year: year,
                            subject: sanitizedFolderName,
                            examType: examType,
                            filePath: pdfPath,
                            docType: docType
                        };

                        const fileId = await uploadFile(fileData);

                        if (docType === "barem") {
                            uploadedFiles[sanitizedFolderName] = { baremId: fileId };
                        } else if (docType === "varianta") {
                            const baremId = uploadedFiles[sanitizedFolderName]?.baremId;
                            if (baremId) {
                                await updateVariantaBarem(fileId, baremId);
                            }
                        }
                    } else {
                        console.log(`File already uploaded: ${pdf}`);
                    }
                } catch (error) {
                    console.error(`Error processing file ${pdf}:`, error);
                }
            }
            await fs.unlink(zipPath);
            console.log('Links: ', links);
        }
    } catch (error) {
        console.error(`Failed to scrape year ${year}: ${error.message}`);
    } finally {
        await browser.close();
    }
};

const scrapeAllYears = async () => {
    const startYear = 2018;
    const endYear = new Date().getFullYear() + 1;
    for (let year = startYear; year <= endYear; year++) {
        await scrapeYear(year, "EN");
        await scrapeYear(year, "BAC");
    }
};

module.exports = {
    scrapeAllYears
}
