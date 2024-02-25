using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Db;

public class TasksList
{
    [MaxLength(100)]
    public required string Title { get; set; }
    
    [MaxLength(250)]
    public required string Description { get; set; }

    public List<TodoTask>? TodoTasks { get; set; }
}