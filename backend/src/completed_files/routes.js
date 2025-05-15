const express = require("express");
const { addCompletedFile } = require("../controllers/completedFilesController");

const router = express.Router();

router.post("/add", addCompletedFile);

module.exports = router;
