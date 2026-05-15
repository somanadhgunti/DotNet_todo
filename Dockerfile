# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY ["TodoManagementAPI.sln", "."]
COPY ["TodoManagementAPI.csproj", "."]
COPY ["TodoManagementAPI.Core/TodoManagementAPI.Core.csproj", "TodoManagementAPI.Core/"]
COPY ["TodoManagementAPI.Data/TodoManagementAPI.Data.csproj", "TodoManagementAPI.Data/"]

# Copy source code (but not tests)
COPY ["Program.cs", "."]
COPY ["appsettings*.json", "."]
COPY ["Controllers/", "Controllers/"]
COPY ["Models/", "Models/"]
COPY ["Properties/", "Properties/"]
COPY ["TodoManagementAPI.Core/", "TodoManagementAPI.Core/"]
COPY ["TodoManagementAPI.Data/", "TodoManagementAPI.Data/"]

# Restore dependencies for API project only
RUN dotnet restore "TodoManagementAPI.csproj"

# Build the API project only (not tests)
RUN dotnet build "TodoManagementAPI.csproj" -c Release -o /app/build --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish "TodoManagementAPI.csproj" -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Install necessary packages
RUN apt-get update && apt-get install -y \
    && rm -rf /var/lib/apt/lists/*

COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Health check
HEALTHCHECK --interval=30s --timeout=3s --start-period=40s --retries=3 \
    CMD curl -f http://localhost:8080/api/todos || exit 1

# Run the application
ENTRYPOINT ["dotnet", "TodoManagementAPI.dll"]
