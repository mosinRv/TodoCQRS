using MediatR;
using TodoWebApi.Db;
using TodoWebApi.DTOs;

namespace TodoWebApi.Commands;

public record AddListCommand(Guid UserId, NewListDto List) : IRequest<TasksList>;