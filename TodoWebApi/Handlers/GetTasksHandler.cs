using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Db;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, IEnumerable<TasksList>>
{
    private readonly AppDbContext _context;

    public GetTasksHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TasksList>> Handle(GetTasksQuery request, CancellationToken ctx)
    {
        if (!(await _context.Users
                .Include(u => u.Lists)!
                .ThenInclude(tasksList => tasksList.TodoTasks)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new UserNotFoundException();

        return user.Lists ?? [];
    }
}