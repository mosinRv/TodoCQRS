using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Commands;
using TodoWebApi.Models;
using TodoWebApi.Queries;

namespace TodoWebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly Mediator _mediator;

    public TodoController(Mediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTodos()
    {
        var todos = await _mediator.Send(new GetTasksQuery());
        return Ok(todos);
    }
    [HttpGet, Route("{id}")]
    public async Task<ActionResult<IEnumerable<TodoTask>>> GetTodoById(int id)
    {
        if(await _mediator.Send(new GetTaskByIdQuery(id)) is {} todo)
            return Ok(todo);

        return NotFound();
    }

    [HttpPost]
    public async Task<ActionResult> CreateTask([FromBody] NewTaskDto request)
    {
        // todo what if not created?
        await _mediator.Send(new AddTaskCommand(request));
        return Created();
    }
}