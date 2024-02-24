using MediatR;
using TodoWebApi.Db;
using TodoWebApi.Models;

namespace TodoWebApi.Queries;

public record GetTaskByIdQuery(int Id) : IRequest<TodoTask>;