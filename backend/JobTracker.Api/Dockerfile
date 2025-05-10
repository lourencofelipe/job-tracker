# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Define working directory for the build process
WORKDIR /src

# Copy all project layers to the container
COPY ./JobTracker.Api ./JobTracker.Api
COPY ./JobTracker.Application ./JobTracker.Application
COPY ./JobTracker.Infrastructure ./JobTracker.Infrastructure

# Restore dependencies for the API project
RUN dotnet restore ./JobTracker.Api/JobTracker.Api.csproj

# Compile and publish the API project
RUN dotnet publish ./JobTracker.Api/JobTracker.Api.csproj -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set working directory for the application in prod
WORKDIR /app

# Copy the published application from the build stage
COPY --from=build /out .

# Define entry point (to run the API)
ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]
