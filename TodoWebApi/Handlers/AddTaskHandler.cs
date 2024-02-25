using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Commands;
using TodoWebApi.Db;

namespace TodoWebApi.Handlers;

public class AddTaskHandler : IRequestHandler<AddTaskCommand, TodoTask>
{
    private readonly AppDbContext _context;
    public AddTaskHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask> Handle(AddTaskCommand request, CancellationToken ctx)
    {
        if (!(await _context.Users
                .Include(u => u.Lists)!
                .ThenInclude(tasksList => tasksList.TodoTasks)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new UserNotFoundException();

        var task = request.Task.FormTaskEntity();
        if (!(user.Lists?.FirstOrDefault(l => l.Title == request.Task.TaskListTitle) is { } list))
            throw new ListNotFoundException();

        if (list.TodoTasks is null) list.TodoTasks = new();
        list.TodoTasks.Add(task);
        await _context.SaveChangesAsync(ctx);
        return task;
    }
}