using MediatR;
using Microsoft.EntityFrameworkCore;
using TodoWebApi.Commands;
using TodoWebApi.Db;

namespace TodoWebApi.Handlers;

public class AddListHandler : IRequestHandler<AddListCommand, TasksList>
{
    private readonly AppDbContext _context;

    public AddListHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TasksList> Handle(AddListCommand request, CancellationToken ctx)
    {
        if (!(await _context.Users
                .Include(u => u.Lists)
                .FirstOrDefaultAsync(u => u.Id == request.UserId, ctx) is { } user))
            throw new UserNotFoundException();

        if (user.Lists is null) user.Lists = [];
        var list = request.List.FormListEntity();
        user.Lists.Add(list);
        await _context.SaveChangesAsync(ctx);
        return list;
    }
}