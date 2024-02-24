using TodoWebApi.Db;

namespace TodoWebApi.Models;

/// <summary>
/// Request to create new <see cref="TodoTask"/>
/// </summary>
public record NewTaskDto()
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsDone { get;  set; }

    public TodoTask FormTaskEntity() => new TodoTask()
    {
        Title = this.Title,
        Description = this.Description,
        IsDone = this.IsDone
    };
}


