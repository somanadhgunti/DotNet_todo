namespace TodoManagementAPI.Data.Repositories;

using Microsoft.EntityFrameworkCore;
using TodoManagementAPI.Core.Entities;
using TodoManagementAPI.Core.Interfaces;
using TodoManagementAPI.Data.Context;

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _context;

    public TodoRepository(TodoDbContext context)
    {
        _context = context;
    }

    public async Task<List<Todo>> GetAllAsync()
    {
        return await _context.Todos.OrderByDescending(t => t.CreatedAt).ToListAsync();
    }

    public async Task<Todo?> GetByIdAsync(int id)
    {
        return await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<Todo>> GetByUserAsync(string userId)
    {
        return await _context.Todos
            .Where(t => t.CreatedBy == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<Todo> AddAsync(Todo todo)
    {
        _context.Todos.Add(todo);
        return todo;
    }

    public async Task<Todo> UpdateAsync(Todo todo)
    {
        _context.Todos.Update(todo);
        return todo;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var todo = await GetByIdAsync(id);
        if (todo == null) return false;

        _context.Todos.Remove(todo);
        return true;
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}
