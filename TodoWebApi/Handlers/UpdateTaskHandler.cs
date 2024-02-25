using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Commands;
using TodoWebApi.Db;

namespace TodoWebApi.Handlers;


public class UpdateTaskHandler : IRequestHandler<UpdateTaskStatusCommand, TodoTask>
{
    private readonly AppDbContext _context;

    public UpdateTaskHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask> Handle(UpdateTaskStatusCommand request, CancellationToken ctx)
    {
        if (!(await _context.Users
                .Include(u => u.Lists)!
                .ThenInclude(tasksList => tasksList.TodoTasks)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new UserNotFoundException();

        var task = user.Lists?.SelectMany(l => l.TodoTasks ?? [])
            .FirstOrDefault(t => t.Id == request.Update.TaskId);
        if (task is null) throw new TaskNotFoundException();

        task.Status = request.Update.UpdatedStatus;
        await _context.SaveChangesAsync(ctx);
        return task;
    }
}