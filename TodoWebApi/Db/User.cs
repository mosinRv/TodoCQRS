using System.ComponentModel.DataAnnotations;

namespace TodoWebApi.Db;

public class User
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(100)]
    public required string NickName { get; set; }

    [MaxLength(100)]
    [EmailAddress]
    public required string Email { get; set; } 

    [MaxLength(100)]
    public required string PasswordHash { get; set; }

    public List<TasksList>? Lists { get; set; }
}