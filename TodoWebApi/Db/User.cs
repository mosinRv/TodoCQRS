using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Db;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }

    public List<TodoTask>? Tasks { get; set; }
}