namespace TodoManagementAPI.Tests;

using AutoMapper;
using Moq;
using Xunit;
using TodoManagementAPI.Core.DTOs;
using TodoManagementAPI.Core.Entities;
using TodoManagementAPI.Core.Interfaces;
using TodoManagementAPI.Core.Mapping;
using TodoManagementAPI.Core.Services;

public class TodoServiceTests
{
    private readonly Mock<ITodoRepository> _mockRepository;
    private readonly TodoService _todoService;
    private readonly IMapper _mapper;

    public TodoServiceTests()
    {
        _mockRepository = new Mock<ITodoRepository>();
        var config = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());
        _mapper = config.CreateMapper();
        _todoService = new TodoService(_mockRepository.Object, _mapper);
    }

    [Fact]
    public async Task GetAllTodosAsync_ReturnsListOfTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Test Todo 1", CreatedBy = "user1", IsCompleted = false },
            new Todo { Id = 2, Title = "Test Todo 2", CreatedBy = "user1", IsCompleted = true }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _todoService.GetAllTodosAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
        Assert.Equal("Test Todo 1", result[0].Title);
    }

    [Fact]
    public async Task GetTodoByIdAsync_WithValidId_ReturnsTodo()
    {
        // Arrange
        var todo = new Todo { Id = 1, Title = "Test Todo", CreatedBy = "user1", IsCompleted = false };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(todo);

        // Act
        var result = await _todoService.GetTodoByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Test Todo", result.Title);
        Assert.Equal(1, result.Id);
    }

    [Fact]
    public async Task GetTodoByIdAsync_WithInvalidId_ReturnsNull()
    {
        // Arrange
        _mockRepository.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Todo?)null);

        // Act
        var result = await _todoService.GetTodoByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task CreateTodoAsync_WithValidData_CreatesTodo()
    {
        // Arrange
        var createDto = new CreateTodoDto 
        { 
            Title = "New Todo", 
            Description = "Test Description",
            Priority = 2
        };
        var todo = new Todo 
        { 
            Id = 1, 
            Title = "New Todo", 
            Description = "Test Description",
            CreatedBy = "user1",
            Priority = 2
        };
        _mockRepository.Setup(r => r.AddAsync(It.IsAny<Todo>())).ReturnsAsync(todo);
        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _todoService.CreateTodoAsync(createDto, "user1");

        // Assert
        Assert.NotNull(result);
        Assert.Equal("New Todo", result.Title);
        Assert.Equal("user1", result.CreatedBy);
    }

    [Fact]
    public async Task UpdateTodoAsync_WithValidData_UpdatesTodo()
    {
        // Arrange
        var existingTodo = new Todo 
        { 
            Id = 1, 
            Title = "Old Title", 
            IsCompleted = false,
            CreatedBy = "user1"
        };
        var updateDto = new UpdateTodoDto { Title = "Updated Title", IsCompleted = true };
        
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingTodo);
        _mockRepository.Setup(r => r.UpdateAsync(It.IsAny<Todo>())).ReturnsAsync(existingTodo);
        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _todoService.UpdateTodoAsync(1, updateDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Updated Title", result.Title);
        Assert.True(result.IsCompleted);
    }

    [Fact]
    public async Task DeleteTodoAsync_WithValidId_DeletesTodo()
    {
        // Arrange
        _mockRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
        _mockRepository.Setup(r => r.SaveChangesAsync()).ReturnsAsync(true);

        // Act
        var result = await _todoService.DeleteTodoAsync(1);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task GetCompletedTodosAsync_ReturnsOnlyCompletedTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Completed 1", IsCompleted = true, CreatedBy = "user1" },
            new Todo { Id = 2, Title = "Pending 1", IsCompleted = false, CreatedBy = "user1" },
            new Todo { Id = 3, Title = "Completed 2", IsCompleted = true, CreatedBy = "user1" }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _todoService.GetCompletedTodosAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, todo => Assert.True(todo.IsCompleted));
    }

    [Fact]
    public async Task GetPendingTodosAsync_ReturnsOnlyPendingTodos()
    {
        // Arrange
        var todos = new List<Todo>
        {
            new Todo { Id = 1, Title = "Completed 1", IsCompleted = true, CreatedBy = "user1" },
            new Todo { Id = 2, Title = "Pending 1", IsCompleted = false, CreatedBy = "user1" },
            new Todo { Id = 3, Title = "Pending 2", IsCompleted = false, CreatedBy = "user1" }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(todos);

        // Act
        var result = await _todoService.GetPendingTodosAsync();

        // Assert
        Assert.Equal(2, result.Count);
        Assert.All(result, todo => Assert.False(todo.IsCompleted));
    }
}
