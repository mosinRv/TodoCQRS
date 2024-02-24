using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Db;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string NickName { get; set; }

    [MaxLength(100)]
    public required string PasswordHash { get; set; }

    public List<TodoTask>? Tasks { get; set; }
}