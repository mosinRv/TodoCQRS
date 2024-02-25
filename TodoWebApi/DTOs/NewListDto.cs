using System.ComponentModel.DataAnnotations;
using TodoWebApi.Db;

namespace TodoWebApi.DTOs;

public record NewListDto
{
    [MaxLength(100)]
    public required string Title { get; set; }
    
    [MaxLength(250)]
    public required string Description { get; set; }

    public TasksList FormListEntity() => new()
    {
        Title = this.Title,
        Description = this.Description,
        TodoTasks = []
    };
}