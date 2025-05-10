# Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
WORKDIR /src

# Copy
COPY . .

WORKDIR /backend/JobTracker.Api

# Restore Dependencies
RUN dotnet restore JobTracker.Api.csproj

# Compiles and publish
RUN dotnet publish JobTracker.Api.csproj -c Release -o /out

# Runtime
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app
COPY --from=build /out .

ENTRYPOINT ["dotnet", "JobTracker.Api.dll"]