# Todo Management API

A professional, production-ready REST API for managing todos built with **ASP.NET Core 8**, **Entity Framework Core**, and following **clean architecture** principles.

## 📋 Overview

This project demonstrates enterprise-level C# development practices including:
- ✅ Clean Architecture (Entities, DTOs, Services, Repositories)
- ✅ SOLID Principles
- ✅ Dependency Injection
- ✅ Entity Framework Core with SQL Server
- ✅ RESTful API Design
- ✅ Swagger/OpenAPI Documentation
- ✅ Comprehensive Error Handling
- ✅ Unit Tests (xUnit & Moq)
- ✅ Async/Await Patterns
- ✅ AutoMapper for DTO Mapping
- ✅ Structured Logging

## 🏗️ Project Structure

```
TodoManagementAPI/
├── TodoManagementAPI/                 # API Layer (Controllers, Program.cs)
├── TodoManagementAPI.Core/            # Domain Layer (Entities, DTOs, Interfaces, Services)
├── TodoManagementAPI.Data/            # Data Access Layer (DbContext, Repositories)
├── TodoManagementAPI.Tests/           # Unit Tests
└── .github/                           # GitHub workflows (CI/CD)
```

## 🔧 Tech Stack

- **Framework**: ASP.NET Core 8.0
- **Database**: SQL Server (LocalDB)
- **ORM**: Entity Framework Core 8.0
- **Mapping**: AutoMapper
- **Testing**: xUnit, Moq
- **Documentation**: Swagger/OpenAPI
- **Logging**: Built-in ASP.NET Core logging

## 📦 Features

### Core Features
- **Create Todos** - Add new tasks with title, description, priority, and due date
- **Read Todos** - Retrieve single or all todos with filtering options
- **Update Todos** - Modify existing todos (partial updates supported)
- **Delete Todos** - Remove todos from the system
- **Filter Todos** - Get completed or pending todos
- **User Tracking** - Track which user created each todo

### API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/todos` | Get all todos |
| GET | `/api/todos/{id}` | Get todo by ID |
| GET | `/api/todos/user/{userId}` | Get todos by user |
| GET | `/api/todos/filter/completed` | Get completed todos |
| GET | `/api/todos/filter/pending` | Get pending todos |
| POST | `/api/todos` | Create new todo |
| PUT | `/api/todos/{id}` | Update todo |
| DELETE | `/api/todos/{id}` | Delete todo |

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK or later
- SQL Server (LocalDB)
- Visual Studio 2022 or VS Code

### Installation

1. **Clone the repository**
```bash
cd "c:\Users\Somu\Desktop\c hash pro\TodoManagementAPI"
```

2. **Restore NuGet packages**
```bash
dotnet restore
```

3. **Update database**
```bash
dotnet ef database update --project TodoManagementAPI.Data
```

4. **Run the application**
```bash
dotnet run --project TodoManagementAPI
```

The API will start at `https://localhost:7200` and Swagger UI at `https://localhost:7200`

## 📖 API Documentation

Once the application is running, visit:
- **Swagger UI**: `https://localhost:7200`
- **JSON Schema**: `https://localhost:7200/swagger/v1/swagger.json`

## 🧪 Running Tests

```bash
dotnet test TodoManagementAPI.Tests
```

## 💡 Usage Examples

### Create a Todo
```bash
curl -X POST https://localhost:7200/api/todos \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Complete project",
    "description": "Finish the todo management API",
    "priority": 3,
    "dueDate": "2026-05-20"
  }'
```

### Get All Todos
```bash
curl https://localhost:7200/api/todos
```

### Update a Todo
```bash
curl -X PUT https://localhost:7200/api/todos/1 \
  -H "Content-Type: application/json" \
  -d '{
    "title": "Updated title",
    "isCompleted": true
  }'
```

### Delete a Todo
```bash
curl -X DELETE https://localhost:7200/api/todos/1
```

## 🏛️ Architecture

### Clean Architecture Layers

**API Layer (Controllers)**
- Handles HTTP requests/responses
- Input validation
- Error handling

**Domain Layer (Core)**
- Business logic
- Entities and DTOs
- Service interfaces
- Mapping profiles

**Data Access Layer**
- Entity Framework DbContext
- Repository pattern implementation
- Database migrations

## 🔐 Best Practices Implemented

1. **Separation of Concerns**: Each layer has a specific responsibility
2. **Dependency Injection**: Loose coupling between components
3. **Repository Pattern**: Abstraction over data access
4. **DTO Pattern**: Data transfer objects for API contracts
5. **Async/Await**: Non-blocking operations
6. **Error Handling**: Comprehensive try-catch and logging
7. **Validation**: Input validation at API level
8. **Unit Testing**: Mocked dependencies with xUnit
9. **Logging**: Structured logging for debugging
10. **API Documentation**: Auto-generated Swagger docs

## 📊 Database Schema

### Todo Table
```sql
CREATE TABLE Todos (
    Id INT PRIMARY KEY IDENTITY(1,1),
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(1000),
    IsCompleted BIT DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME DEFAULT GETUTCDATE(),
    DueDate DATETIME,
    Priority INT DEFAULT 1,
    CreatedBy NVARCHAR(100) NOT NULL
);
```

## 🎯 Deployment

### Deploy to Azure
```bash
dotnet publish -c Release
```

### Docker Support
Can be containerized and deployed to Docker/Kubernetes

### Deploy to IIS
Publish as self-contained or framework-dependent deployment

## 📝 License

This project is open source and available under the MIT License.

## 👨‍💻 Author

Created for job application portfolio demonstration

## 🤝 Support

For questions or issues, please open an issue in the repository.

---

**Ready to Deploy!** This project is production-ready and demonstrates professional C# development practices suitable for enterprise environments.
