using MediatR;
using TodoWebApi.Db;

namespace TodoWebApi.Queries;

public record GetTasksQuery(Guid UserId) : IRequest<IEnumerable<TasksList>>;