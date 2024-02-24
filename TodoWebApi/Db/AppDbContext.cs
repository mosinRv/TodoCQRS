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
                PasswordHash = Pwd,
                Tasks =
                [
                    new TodoTask()
                    {
                        Title = "Wash car",
                        Description = "I like when my car is clean. So, it need some cleaning",
                        IsDone = false
                    },
                    new TodoTask()
                    {
                        Title = "Dinner",
                        Description = "Order some Chinese food for dinner",
                        IsDone = false
                    }
                ]
            },
            new()
            {
                NickName = "Bob",
                PasswordHash = Pwd,
                Tasks = new List<TodoTask>()
                {
                    new()
                    {
                        Title = "Take a shower",
                        IsDone = true,
                        Description = "Try new shampoo =D"
                    }
                },
            }
        };

        await context.Users.AddRangeAsync(users);
        await context.SaveChangesAsync();
    }
}