using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TodoWebApi.Db;
using TaskStatus = TodoWebApi.Db.TaskStatus;

namespace TodoTesting;

public sealed class TestSqLiteDbContext : AppDbContext
{
    public TestSqLiteDbContext()
    {
        Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=testing_todoapp.db");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
    }
}

public static class DbTestExtensions
{
    private const string Pwd = "Qweqwe_123";

    public static User[] DefaultUsers = new User[]
    {
        new()
        {
            NickName = "Alice",
            Email = "abc@def.com",
            PasswordHash = Pwd,
            Lists = 
            [
                new TasksList()
                {
                    Title = "First List",
                    Description = "Do this and that",
                    TodoTasks =
                    [
                        new TodoTask()
                        {
                            Title = "Wash car",
                            Description = "I like when my car is clean. So, it need some cleaning",
                            Status = TaskStatus.Working
                        },
                        new TodoTask()
                        {
                            Title = "Dinner",
                            Description = "Order some Chinese food for dinner",
                            Status = TaskStatus.Waiting
                        }
                    ]
                }
            ]
        },
        new()
        {
            NickName = "Bob",
            Email = "bob@def.com",
            PasswordHash = Pwd,
        }
    };
        
    public static async Task FillWithDataAsync(this AppDbContext context)
    {
        await context.Users.AddRangeAsync(DefaultUsers);
        await context.SaveChangesAsync();
    }
}