const express = require("express");
const { getUser, addUser} = require("./controller");

const router = express.Router();

router.post("/", addUser);


module.exports = router;
