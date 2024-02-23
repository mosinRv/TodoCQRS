using MediatR;
using TodoWebApi.Models;

namespace TodoWebApi.Queries;

public record GetTaskByIdQuery(int Id) : IRequest<TodoTask>;