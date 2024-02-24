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
        if (!(await _context.Users.FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new Exception("user not fund");

        var task = request.Task.FormTaskEntity();
        user.Tasks!.Add(task);
        await _context.SaveChangesAsync(ctx);
        return task;
    }
}