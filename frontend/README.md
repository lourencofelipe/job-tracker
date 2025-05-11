# JobTracker Frontend

This is the frontend for the JobTracker app, built using **React and Next.js**.

## Features
- Create, update, delete and list job applications
- Connected to backend API via Axios
- Fully deployed on Vercel

## Live Demo
Frontend: [https://job-tracker-hazel-seven.vercel.app](https://job-tracker-hazel-seven.vercel.app)

## Run Locally

1. Make sure you have **Node.js** installed.
2. Go to frontend folder and install dependencies:
```bash
cd frontend
npm install
```
3. Run the development server:
```bash
npm run dev
```
App runs at: `http://localhost:3000`

## Configuration

Create a `.env` file in the root of the `frontend` folder:

```js
NEXT_PUBLIC_API_URL=http://localhost:5000/api/JobApplication
```

The `.NEXT_PUBLIC_API_URL ` is used in `.src/api/jobApi.js` to make request to the backend API.
