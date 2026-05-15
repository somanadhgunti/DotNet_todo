# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files
COPY ["TodoManagementAPI/TodoManagementAPI.csproj", "TodoManagementAPI/"]
COPY ["TodoManagementAPI.Core/TodoManagementAPI.Core.csproj", "TodoManagementAPI.Core/"]
COPY ["TodoManagementAPI.Data/TodoManagementAPI.Data.csproj", "TodoManagementAPI.Data/"]

# Restore dependencies
RUN dotnet restore "TodoManagementAPI/TodoManagementAPI.csproj"

# Copy all source code
COPY . .

# Build the project
RUN dotnet build "TodoManagementAPI/TodoManagementAPI.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "TodoManagementAPI/TodoManagementAPI.csproj" -c Release -o /app/publish

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
