using MediatR;
using TodoWebApi.Db;

namespace TodoWebApi.Queries;

public record GetTaskByIdQuery(Guid UserId, Guid TaskId) : IRequest<TodoTask?>;