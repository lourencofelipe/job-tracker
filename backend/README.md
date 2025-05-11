# JobTracker.API

This is the backend for the JobTracker application, built using **.NET 9**.

## Features
- CRUD operations for Job Applications
- EF Core In-Memory Database
- Swagger UI for API documentation
- AutoMapper
- Unit testing with xUnit and FluentAssertions

## Live Demo
Swagger: [https://job-tracker-ro0e.onrender.com/swagger/index.html](https://job-tracker-ro0e.onrender.com/swagger/index.html)

## Run Locally

1. Make sure you have [.NET 9 SDK](https://dotnet.microsoft.com/) installed.
2. Run the API:
```bash
dotnet run --project JobTracker.Api
```
3. Access Swagger UI:  
[http://localhost:5000/swagger](http://localhost:5000/swagger)

## Run Tests
```bash
dotnet test
```
Test project: `JobTracker.Test`
