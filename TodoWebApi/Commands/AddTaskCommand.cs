using MediatR;
using TodoWebApi.Db;
using TodoWebApi.DTOs;

namespace TodoWebApi.Commands;

public record AddTaskCommand(Guid UserId, NewTaskDto Task) : IRequest<TodoTask>;