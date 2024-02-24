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
        var user = await _context.Users.AsNoTracking().Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx);
        if(user is null) throw new Exception("User not Found");

        return user.Tasks?.FirstOrDefault(t => t.Id == request.TaskId);
    }
}