# Todo Management API - Project Setup Instructions

## Quick Start

This is a professional, enterprise-ready C# REST API project demonstrating clean architecture, SOLID principles, and industry best practices.

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB) - Installed with Visual Studio
- Visual Studio 2022 or VS Code

### Setup Steps

1. **Restore Dependencies**
```bash
dotnet restore
```

2. **Create Database**
```bash
dotnet ef database update --project TodoManagementAPI.Data
```

3. **Run the Application**
```bash
dotnet run --project TodoManagementAPI
```

The application will start at `https://localhost:7200` with Swagger UI enabled.

### Run Tests
```bash
dotnet test
```

## Project Highlights

✅ **Clean Architecture** - Separated into API, Core (Domain), and Data layers  
✅ **SOLID Principles** - Well-designed interfaces and dependency injection  
✅ **Entity Framework Core** - Async database access with SQL Server  
✅ **RESTful API** - Professional endpoint design with proper HTTP verbs  
✅ **Swagger/OpenAPI** - Auto-generated interactive API documentation  
✅ **Unit Tests** - Comprehensive tests using xUnit and Moq  
✅ **Error Handling** - Structured exception handling and logging  
✅ **AutoMapper** - Clean DTO mapping patterns  
✅ **Production Ready** - Deployment guide and security best practices included  

## Project Structure

```
TodoManagementAPI/
├── TodoManagementAPI/           # API Layer with Controllers
├── TodoManagementAPI.Core/      # Domain Layer - Business Logic
├── TodoManagementAPI.Data/      # Data Access Layer - EF Core
├── TodoManagementAPI.Tests/     # Unit Tests
├── README.md                    # Full documentation
└── DEPLOYMENT.md                # Deployment guide
```

## Key Features

- ✅ Create, Read, Update, Delete Todos
- ✅ Filter by completion status
- ✅ User-based todo management
- ✅ Priority and due date support
- ✅ Complete API documentation
- ✅ Database migrations
- ✅ Logging and error handling
- ✅ CORS enabled for integration

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Get all todos |
| GET | `/api/todos/{id}` | Get todo by ID |
| POST | `/api/todos` | Create new todo |
| PUT | `/api/todos/{id}` | Update todo |
| DELETE | `/api/todos/{id}` | Delete todo |
| GET | `/api/todos/filter/completed` | Get completed todos |
| GET | `/api/todos/filter/pending` | Get pending todos |

## Technology Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core 8.0
- **Mapping**: AutoMapper
- **Testing**: xUnit with Moq
- **Documentation**: Swagger/OpenAPI
- **Language**: C# 12

## Deployment

See [DEPLOYMENT.md](DEPLOYMENT.md) for:
- IIS deployment
- Docker containerization
- Azure App Service deployment
- Cloud deployment options

## Contact & Support

For questions about this project, refer to the README.md for detailed documentation.

---

**Status**: ✅ Ready for job application review  
**Last Updated**: 2026-05-15
