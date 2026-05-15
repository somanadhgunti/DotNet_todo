namespace TodoManagementAPI.Core.Interfaces;

using TodoManagementAPI.Core.DTOs;
using TodoManagementAPI.Core.Entities;

public interface ITodoService
{
    Task<List<TodoDto>> GetAllTodosAsync();
    Task<TodoDto?> GetTodoByIdAsync(int id);
    Task<List<TodoDto>> GetUserTodosAsync(string userId);
    Task<TodoDto> CreateTodoAsync(CreateTodoDto createDto, string userId);
    Task<TodoDto?> UpdateTodoAsync(int id, UpdateTodoDto updateDto);
    Task<bool> DeleteTodoAsync(int id);
    Task<List<TodoDto>> GetCompletedTodosAsync();
    Task<List<TodoDto>> GetPendingTodosAsync();
}
