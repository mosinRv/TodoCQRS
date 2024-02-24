using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Db;

public class User
{
    [Key]
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public bool IsCool { get; set; }
    public TodoTask[]? Tasks { get; set; }
}