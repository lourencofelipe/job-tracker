# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Set the working directory
WORKDIR /app

# Copy only the necessary files
COPY ./JobTracker.Api ./JobTracker.Api
COPY ./JobTracker.Application ./JobTracker.Application
COPY ./JobTracker.Infrastructure ./JobTracker.Infrastructure

# Restore dependencies
RUN dotnet restore "JobTracker.Api.csproj"

# Compile and publish the application
RUN dotnet publish "JobTracker.Api.csproj" -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

# Set the working directory
WORKDIR /app

# Copy the published output from the build stage
COPY --from=build /out .

# Set the entry point for the container
ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]
