using MediatR;
using TodoWebApi.Commands;

namespace TodoWebApi.Handlers;

public class AddTaskHandler : IRequestHandler<AddTaskCommand>
{
    public Task Handle(AddTaskCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}