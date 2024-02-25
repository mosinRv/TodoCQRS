using MediatR;
using TodoWebApi.Db;
using TodoWebApi.DTOs;
using TaskStatus = TodoWebApi.Db.TaskStatus;

namespace TodoWebApi.Commands;

public record UpdateTaskStatusCommand(Guid UserId, UpdateTaskDto Update) : IRequest<TodoTask>;