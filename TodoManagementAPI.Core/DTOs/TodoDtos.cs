namespace TodoManagementAPI.Core.DTOs;

public class CreateTodoDto
{
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; } = 1;
}

public class UpdateTodoDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public bool? IsCompleted { get; set; }
    public DateTime? DueDate { get; set; }
    public int? Priority { get; set; }
}

public class TodoDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsCompleted { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime? DueDate { get; set; }
    public int Priority { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}
