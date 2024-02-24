using MediatR;
using TodoWebApi.Db;
using TodoWebApi.Models;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, TodoTask>
{
    public Task<TodoTask> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}