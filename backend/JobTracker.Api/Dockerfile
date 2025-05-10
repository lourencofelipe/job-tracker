# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and all project folders
COPY ../JobTracker.sln ./
COPY ../JobTracker.Api ./JobTracker.Api
COPY ../JobTracker.Application ./JobTracker.Application
COPY ../JobTracker.Infrastructure ./JobTracker.Infrastructure

# Restore and publish from API project
WORKDIR /src/JobTracker.Api
RUN dotnet restore
RUN dotnet publish -c Release -o /out

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]
