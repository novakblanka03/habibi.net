const express = require("express");
const cors = require("cors");
const fileRoutes = require("./src/files/routes");
const { scrapeAllYears } = require("./src/scrape");
const { AppDataSource } = require("./db");
const {useOpenAi} = require("./openai-api.json")
const fs = require("fs").promises;
const userRoutes = require("./src/users/routes");

const app = express();
const port = 3000;

app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

app.use("/files", fileRoutes);

app.use("/users", userRoutes);

app.get("/", (req, res) => {
  res.send({ text: "Hello World!" });
});

app.get("/scrape", async (req, res) => {
  try {
    console.log("Starting scraping...");
    await scrapeAllYears();
    res.send("Scraping started. Check the logs for progress.");
  } catch (error) {
    console.error("Error starting scraping:", error);
    res.status(500).send("Error starting scraping");
  }
});

app.get("/evaluation", async (req, res) => {
  try {
    console.log("Starting evaluation...");
    const { evaluateResponse } = useOpenAi();

    const topicFile = './src/tetel-text.txt';
    const baremFile = './src/barem-text.txt';
    const userResponseFile = './src/image-text.txt';

    const response = await evaluateResponse(topicFile, baremFile, userResponseFile);

    if (response) {
      res.json(response);
    } else {
      res.status(500).send("Error in AI evaluation");
    }
  } catch (error) {
    console.error("Error starting evaluation:", error);
    res.status(500).send("Error during evaluation");
  }
});

// app.get("/evaluated", async (req, res) => {
//   try {
//     const outputFile = "./src/output.txt";
//     const fileContent = await fs.readFile(outputFile, "utf-8");

//     // let parsedJson;
//     // try {
//     //     parsedJson = JSON.parse(fileContent);
//     // } catch (error) {
//     //     console.error("Hiba a JSON feldolgozásakor:", error);
//     //     return res.status(500).json({ error: "Hibás JSON formátum az output.txt-ben" });
//     // }

//     res.json(fileContent);
//   } catch (error) {
//     console.error("Hiba az output.txt beolvasásakor:", error);
//     res.status(500).json({ error: "Nem sikerült beolvasni az output.txt fájlt" });
// }
// });

app.post('/uploadPhotos', (req, res) => {
  console.log('Received files:', req.files);
  res.send('Files received');
  });

AppDataSource.initialize()
  .then(() => {
    console.log("Database connected successfully!");

    app.listen(port, () => {
      console.log(`Server listening at http://localhost:${port}`);
    });
  })
  .catch((error) => {
    console.error("Error initializing database:", error);
    process.exit(1); // Exit if DB fails to connect
  });
