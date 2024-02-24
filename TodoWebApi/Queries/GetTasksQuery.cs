using MediatR;
using TodoWebApi.Db;
using TodoWebApi.Models;

namespace TodoWebApi.Queries;

public record GetTasksQuery : IRequest<IEnumerable<TodoTask>>;