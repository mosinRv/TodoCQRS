using Microsoft.EntityFrameworkCore;

namespace TodoWebApi.Db;

public abstract class AppDbContext : DbContext
{
    public DbSet<TodoTask> TodoTasks { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;
}

public sealed class SqLiteDbContext : AppDbContext
{
    public SqLiteDbContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=todoapp.db");
        optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
    }
}

public static class DbExtension
{
    private const string Pwd = "Qweqwe_123";
    public static async Task FillWithData(this AppDbContext context)
    {
        var users = new User[]
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

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}