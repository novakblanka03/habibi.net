const admin = require('../../firebase-config');
const {AppDataSource} = require("../../db");
const User = require("./entity");

const userRepository = AppDataSource.getRepository(User);

const addUser = async (req, res) => {
    try {
        const { uid, email } = req.body;

        if (!uid || !email) {
            return res.status(400).json({ error: "Missing required fields: id and email" });
        }

        // Check if user already exists
        let user = await userRepository.findOne({ where: { uid } });

        if (!user) {
            // Create new user
            user = userRepository.create({ uid, email });
            await userRepository.save(user);
        }

        res.status(201).json(user);
    } catch (error) {
        console.error("Error adding user:", error);
        res.status(500).json({ error: "Failed to add user" });
    }
};

module.exports = { addUser };
