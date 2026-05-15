# Solutions and Deployment Guide

## Local Development

### Using Visual Studio 2022
1. Open the solution file `TodoManagementAPI.sln`
2. Right-click on TodoManagementAPI project → Set as Startup Project
3. Open Package Manager Console
4. Run: `Update-Database`
5. Press F5 to debug

### Using VS Code
1. Open the workspace folder
2. Install C# Dev Kit extension
3. Terminal: `dotnet restore`
4. Terminal: `dotnet ef database update --project TodoManagementAPI.Data`
5. Terminal: `dotnet run --project TodoManagementAPI`

## Database Setup

### SQL Server LocalDB
```bash
# If LocalDB is not running, it will start automatically
sqlcmd -S (localdb)\mssqllocaldb -Q "SELECT @@VERSION"
```

### Create from Scratch
```bash
# Create migrations
dotnet ef migrations add InitialCreate --project TodoManagementAPI.Data

# Apply migrations
dotnet ef database update --project TodoManagementAPI.Data
```

## Testing

### Run All Tests
```bash
dotnet test
```

### Run Specific Test Class
```bash
dotnet test --filter "ClassName=TodoServiceTests"
```

### With Coverage Report
```bash
dotnet test /p:CollectCoverage=true
```

## Publishing for Deployment

### Windows Server / IIS
```bash
dotnet publish -c Release -o .\publish
```

### Docker
Create a Dockerfile in the root:
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=builder /app/publish .
ENTRYPOINT ["dotnet", "TodoManagementAPI.dll"]
```

### Azure App Service
```bash
dotnet publish -c Release --self-contained false
```

### Heroku / Cloud Deployment
The application uses environment variables:
- `ASPNETCORE_ENVIRONMENT` - Set to "Production"
- `ConnectionStrings__DefaultConnection` - Database connection string

## Environment Configuration

### Development (appsettings.Development.json)
- Debug logging enabled
- Swagger enabled
- CORS allows all origins

### Production (appsettings.json or appsettings.Production.json)
- Minimal logging
- Swagger disabled
- CORS restricted
- HTTPS enforced

## Performance Optimization

1. **Database Indexing**: Applied on CreatedBy, IsCompleted, CreatedAt
2. **Async Operations**: All I/O operations are async
3. **Connection Pooling**: Entity Framework handles pooling
4. **Lazy Loading Disabled**: All queries explicit

## Security Considerations

1. **SQL Injection**: Prevented by Entity Framework parameterized queries
2. **CORS**: Configure allowed origins in production
3. **Authentication**: Can be added via JWT middleware
4. **Input Validation**: ModelState validation in controllers
5. **HTTPS**: Required in production

## Troubleshooting

### Database Connection Error
```bash
# Reset LocalDB
SqlLocalDB.exe stop MSSQLLocalDB
SqlLocalDB.exe start MSSQLLocalDB
```

### Port Already in Use
Edit `launchSettings.json` and change the port number

### Package Version Conflicts
```bash
dotnet clean
dotnet restore
```
