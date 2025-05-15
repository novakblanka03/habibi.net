const express = require("express");
const router = express.Router();
const { getAllFiles, getFile } = require("./controller");

router.get("/", getAllFiles);

router.get("/file/:id", getFile);

module.exports = router;
