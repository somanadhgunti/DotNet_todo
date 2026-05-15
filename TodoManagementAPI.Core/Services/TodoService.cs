namespace TodoManagementAPI.Core.Services;

using AutoMapper;
using TodoManagementAPI.Core.DTOs;
using TodoManagementAPI.Core.Entities;
using TodoManagementAPI.Core.Interfaces;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;
    private readonly IMapper _mapper;

    public TodoService(ITodoRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<List<TodoDto>> GetAllTodosAsync()
    {
        var todos = await _repository.GetAllAsync();
        return _mapper.Map<List<TodoDto>>(todos);
    }

    public async Task<TodoDto?> GetTodoByIdAsync(int id)
    {
        var todo = await _repository.GetByIdAsync(id);
        return todo == null ? null : _mapper.Map<TodoDto>(todo);
    }

    public async Task<List<TodoDto>> GetUserTodosAsync(string userId)
    {
        var todos = await _repository.GetByUserAsync(userId);
        return _mapper.Map<List<TodoDto>>(todos);
    }

    public async Task<TodoDto> CreateTodoAsync(CreateTodoDto createDto, string userId)
    {
        var todo = _mapper.Map<Todo>(createDto);
        todo.CreatedBy = userId;
        todo.CreatedAt = DateTime.UtcNow;
        todo.UpdatedAt = DateTime.UtcNow;

        await _repository.AddAsync(todo);
        await _repository.SaveChangesAsync();

        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<TodoDto?> UpdateTodoAsync(int id, UpdateTodoDto updateDto)
    {
        var todo = await _repository.GetByIdAsync(id);
        if (todo == null) return null;

        if (!string.IsNullOrEmpty(updateDto.Title))
            todo.Title = updateDto.Title;

        if (updateDto.Description != null)
            todo.Description = updateDto.Description;

        if (updateDto.IsCompleted.HasValue)
            todo.IsCompleted = updateDto.IsCompleted.Value;

        if (updateDto.DueDate.HasValue)
            todo.DueDate = updateDto.DueDate;

        if (updateDto.Priority.HasValue)
            todo.Priority = updateDto.Priority.Value;

        todo.UpdatedAt = DateTime.UtcNow;

        await _repository.UpdateAsync(todo);
        await _repository.SaveChangesAsync();

        return _mapper.Map<TodoDto>(todo);
    }

    public async Task<bool> DeleteTodoAsync(int id)
    {
        var result = await _repository.DeleteAsync(id);
        if (result)
            await _repository.SaveChangesAsync();
        return result;
    }

    public async Task<List<TodoDto>> GetCompletedTodosAsync()
    {
        var todos = await _repository.GetAllAsync();
        var completed = todos.Where(t => t.IsCompleted).ToList();
        return _mapper.Map<List<TodoDto>>(completed);
    }

    public async Task<List<TodoDto>> GetPendingTodosAsync()
    {
        var todos = await _repository.GetAllAsync();
        var pending = todos.Where(t => !t.IsCompleted).ToList();
        return _mapper.Map<List<TodoDto>>(pending);
    }
}
