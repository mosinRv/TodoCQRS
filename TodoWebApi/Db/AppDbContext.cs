using Microsoft.EntityFrameworkCore;
using TodoWebApi.Models;

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