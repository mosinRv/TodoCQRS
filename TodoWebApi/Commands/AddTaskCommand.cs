using MediatR;
using TodoWebApi.Models;

namespace TodoWebApi.Commands;

public record AddTaskCommand(NewTaskDto task) : IRequest;