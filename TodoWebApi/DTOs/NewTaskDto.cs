using TodoWebApi.Db;
using TaskStatus = TodoWebApi.Db.TaskStatus;

namespace TodoWebApi.DTOs;


/// <summary>
/// Request to create new <see cref="TodoTask"/>
/// </summary>
public record NewTaskDto()
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public TaskStatus Status { get;  set; }
    public required string TaskListTitle { get; set; }

    public TodoTask FormTaskEntity() => new()
    {
        Title = this.Title,
        Description = this.Description,
        Status = this.Status
    };
}