using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Db;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, IEnumerable<TodoTask>>
{
    private readonly AppDbContext _context;

    public GetTasksHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TodoTask>> Handle(GetTasksQuery request, CancellationToken ctx)
    {
        var user = await _context.Users.AsNoTracking().Include(u => u.Tasks)
            .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx);
        if(user is null) throw new Exception("Not Found");

        return user.Tasks ?? [];
    }
}