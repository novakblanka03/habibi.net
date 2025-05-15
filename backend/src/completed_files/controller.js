const { AppDataSource } = require("../../db");
const CompletedFiles = require("./entity");

const completedFilesRepository = AppDataSource.getRepository(CompletedFiles);

const addCompletedFile = async (req, res) => {
    try {
        const { file_id, user_id, finish_time, date, percentage, evaluation, notes } = req.body;

        if (!file_id || !user_id || !finish_time || !date || percentage === undefined) {
            return res.status(400).json({ error: "Missing required fields" });
        }

        const completedFile = completedFilesRepository.create({
            file_id,
            user_id,
            finish_time,
            date,
            percentage,
            evaluation: evaluation || {}, 
            notes: notes || {} 
        });

        await completedFilesRepository.save(completedFile);

        res.status(201).json(completedFile);
    } catch (error) {
        console.error("Error adding completed file:", error);
        res.status(500).json({ error: "Failed to add completed file" });
    }
};

module.exports = { addCompletedFile };
