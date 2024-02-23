using MediatR;
using TodoWebApi.Models;

namespace TodoWebApi.Queries;

public record GetTasksQuery : IRequest<IEnumerable<TodoTask>>;