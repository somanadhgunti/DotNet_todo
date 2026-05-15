# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy everything
COPY . .

# Restore NuGet packages
RUN dotnet restore

# Build the project (Release mode)
RUN dotnet build -c Release --no-restore

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish --no-build

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy published files
COPY --from=publish /app/publish .

# Expose port
EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

# Run the application
ENTRYPOINT ["dotnet", "TodoManagementAPI.dll"]
