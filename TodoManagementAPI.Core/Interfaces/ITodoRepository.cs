namespace TodoManagementAPI.Core.Interfaces;

using TodoManagementAPI.Core.Entities;

public interface ITodoRepository
{
    Task<List<Todo>> GetAllAsync();
    Task<Todo?> GetByIdAsync(int id);
    Task<List<Todo>> GetByUserAsync(string userId);
    Task<Todo> AddAsync(Todo todo);
    Task<Todo> UpdateAsync(Todo todo);
    Task<bool> DeleteAsync(int id);
    Task<bool> SaveChangesAsync();
}
