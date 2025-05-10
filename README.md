# JobTracker

JobTracker is an application for managing job applications.  
Built with **.NET 9 (API)** and **React / Next.js (Frontend)**.

## Project Structure

- `JobTracker.Api` - ASP.NET Core Web API
- `JobTracker.Application` - Application layer with business logic
- `JobTracker.Domain` - Core entities and enums
- `JobTracker.Infrastructure` - EF Core In-Memory DB & repositories
- `JobTracker.Test` - xUnit test project
- `frontend` - React / Next.js frontend

## Features

-  Create, Update, and Delete job applications
-  Track application status: `Applied`, `Interview`, `Offer` and, `Rejected`
-  Doucmented API with Swagger
-  Deployment-ready configuration

## Deployments

The application is deployed and accessible via the following links:

### Backend (.NET 9 - Hosted on Render)

 - Swagger (local): http://localhost:5000/swagger

 - Swagger (deployed): https://job-tracker-ro0e.onrender.com/swagger/index.html

### Frontend (React + Next.js - Hosted on Vercel)

 - Live Site: https://job-tracker-hazel-seven.vercel.app/

## Technologies Used

### Backend:
- .NET 9
- ASP.NET Core Web API
- AutoMapper
- EF Core In-Memory
- xUnit + FluentAssertions

### Frontend:
- React
- Next.js
- Axios

## How to run locally

### Backend:
```bash
cd JobTracker.Api
dotnet run
```

Swagger will be available at: `http://localhost:5000/swagger`

### Frontend:
```bash
cd frontend
npm install
npm run dev
```

Frontend will run on: `http://localhost:3000`
