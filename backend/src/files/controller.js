const {AppDataSource} = require("../../db");
const fs = require("fs");
const File = require("./entity");

const fileRepository = AppDataSource.getRepository(File);

const getAllFiles = async (req, res) => {
    try {
        const {examType, year, subject} = req.query;
        let query = fileRepository.createQueryBuilder("file")
            .select(["file.id", "file.name", "file.subject", "file.baremId", "file.year", "file.examType"])
            .where("file.docType = :docType", { docType: "varianta" })
            .orderBy("file.year", "DESC");

        if (examType) query = query.andWhere("file.examType = :examType", {examType});
        if (year) query = query.andWhere("file.year = :year", {year});
        if (subject) query = query.andWhere("file.subject ILIKE :subject", {subject: `%${subject}%`});

        if (!["ea_limba_si_literatura_romana", "limba_si_literatura_romana"].includes(subject)) {
            query = query.take(10);
        } else {
            query = query.take(20);
        }

        const files = await query.getMany();
        res.status(200).json(files);
    } catch (error) {
        console.error("Error fetching files:", error);
        res.status(500).json({error: "Failed to fetch files"});
    }
};


const getFile = async (req, res) => {
    try {
        const {id} = req.params; // Get file name from request params
        const file = await fileRepository.findOne({where: {id}});

        if (!file) {
            return res.status(404).json({error: "File not found"});
        }

        res.setHeader("Content-Type", "application/pdf");
        res.setHeader("Content-Disposition", `inline; filename="${file.name}"`); // Display in browser
        res.send(file.data); // Send the file buffer as a response
    } catch (error) {
        console.error("Error fetching file:", error);
        res.status(500).json({error: "Failed to fetch file"});
    }
};

const uploadFile = async (fileData) => {
    try {
        const {name, year, subject, examType, filePath, docType} = fileData;
        const fileBuffer = fs.readFileSync(filePath);

        const newFile = fileRepository.create({
            name,
            data: fileBuffer,
            year,
            subject,
            examType,
            docType,
            createdAt: new Date(),
        });

        const savedFile = await fileRepository.save(newFile);
        console.log(`File added with ID: ${savedFile.id}`);
        return savedFile.id;
    } catch (error) {
        console.error("Error uploading file:", error);
        throw new Error("Error uploading file");
    }
};

const updateVariantaBarem = async (variantaId, baremId) => {
    const fileRepo = AppDataSource.getRepository(File);
    await fileRepo.update({ id: variantaId }, { baremId });
};

const isFileUploaded = async (name) => {
    try {
        const file = await fileRepository.findOne({where: {name}});
        return !!file;
    } catch (error) {
        console.error("Error checking if file is uploaded:", error);
        throw error;
    }
};

module.exports = {
    getAllFiles,
    getFile,
    uploadFile,
    isFileUploaded,
    updateVariantaBarem,
};
