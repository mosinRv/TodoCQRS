using MediatR;
using TodoWebApi.Models;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetTasksHandler : IRequestHandler<GetTasksQuery, IEnumerable<TodoTask>>
{
    public Task<IEnumerable<TodoTask>> Handle(GetTasksQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}