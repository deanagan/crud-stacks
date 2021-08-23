import express from 'express';

const app = express();
const PORT = 8081;
app.get('/hello', (req, res) => res.send('Hello basic setup!'));
app.listen(PORT, () => {
  console.log(`[server]: Server is running at http://localhost:${PORT}`);
});
