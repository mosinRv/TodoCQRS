using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Db;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TodoTask?>
{
    private readonly AppDbContext _context;

    public GetTaskByIdHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TodoTask?> Handle(GetTaskByIdQuery request, CancellationToken ctx)
    {
        if (!(await _context.Users
                .Include(u => u.Lists)!
                .ThenInclude(tasksList => tasksList.TodoTasks)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new UserNotFoundException();

        var task = user.Lists?.SelectMany(l => l.TodoTasks ?? [])
            .FirstOrDefault(t => t.Id == request.TaskId);

        return task;
    }
}