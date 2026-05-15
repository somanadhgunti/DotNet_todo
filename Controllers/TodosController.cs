namespace TodoManagementAPI.Controllers;

using Microsoft.AspNetCore.Mvc;
using TodoManagementAPI.Core.DTOs;
using TodoManagementAPI.Core.Interfaces;

[ApiController]
[Route("api/[controller]")]
public class TodosController : ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly ILogger<TodosController> _logger;

    public TodosController(ITodoService todoService, ILogger<TodosController> logger)
    {
        _todoService = todoService;
        _logger = logger;
    }

    /// <summary>
    /// Get all todos
    /// </summary>
    /// <returns>List of all todos</returns>
    [HttpGet]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllTodos()
    {
        try
        {
            var todos = await _todoService.GetAllTodosAsync();
            _logger.LogInformation("Retrieved all todos. Count: {TodoCount}", todos.Count);
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving todos");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get a specific todo by ID
    /// </summary>
    /// <param name="id">Todo ID</param>
    /// <returns>Todo details</returns>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetTodoById(int id)
    {
        try
        {
            var todo = await _todoService.GetTodoByIdAsync(id);
            if (todo == null)
            {
                _logger.LogWarning("Todo with ID {TodoId} not found", id);
                return NotFound(new { message = "Todo not found" });
            }
            return Ok(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving todo {TodoId}", id);
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get todos for a specific user
    /// </summary>
    /// <param name="userId">User ID</param>
    /// <returns>User's todos</returns>
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserTodos(string userId)
    {
        try
        {
            var todos = await _todoService.GetUserTodosAsync(userId);
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving todos for user {UserId}", userId);
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get completed todos
    /// </summary>
    /// <returns>List of completed todos</returns>
    [HttpGet("filter/completed")]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCompletedTodos()
    {
        try
        {
            var todos = await _todoService.GetCompletedTodosAsync();
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving completed todos");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Get pending todos
    /// </summary>
    /// <returns>List of pending todos</returns>
    [HttpGet("filter/pending")]
    [ProducesResponseType(typeof(List<TodoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetPendingTodos()
    {
        try
        {
            var todos = await _todoService.GetPendingTodosAsync();
            return Ok(todos);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving pending todos");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Create a new todo
    /// </summary>
    /// <param name="createDto">Todo details</param>
    /// <returns>Created todo</returns>
    [HttpPost]
    [ProducesResponseType(typeof(TodoDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTodo([FromBody] CreateTodoDto createDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.Identity?.Name ?? "Anonymous";
            var todo = await _todoService.CreateTodoAsync(createDto, userId);
            _logger.LogInformation("Todo created with ID: {TodoId}", todo.Id);
            return CreatedAtAction(nameof(GetTodoById), new { id = todo.Id }, todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating todo");
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Update an existing todo
    /// </summary>
    /// <param name="id">Todo ID</param>
    /// <param name="updateDto">Updated todo details</param>
    /// <returns>Updated todo</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(TodoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto updateDto)
    {
        try
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var todo = await _todoService.UpdateTodoAsync(id, updateDto);
            if (todo == null)
                return NotFound(new { message = "Todo not found" });

            _logger.LogInformation("Todo with ID {TodoId} updated", id);
            return Ok(todo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating todo {TodoId}", id);
            return StatusCode(500, new { message = "Internal server error" });
        }
    }

    /// <summary>
    /// Delete a todo
    /// </summary>
    /// <param name="id">Todo ID</param>
    /// <returns>Success message</returns>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteTodo(int id)
    {
        try
        {
            var result = await _todoService.DeleteTodoAsync(id);
            if (!result)
                return NotFound(new { message = "Todo not found" });

            _logger.LogInformation("Todo with ID {TodoId} deleted", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting todo {TodoId}", id);
            return StatusCode(500, new { message = "Internal server error" });
        }
    }
}
