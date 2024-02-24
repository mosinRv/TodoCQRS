using MediatR;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Commands;
using TodoWebApi.Db;
using TodoWebApi.DTOs;
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

    /// <summary>
    /// Get tasks of the user
    /// </summary>
    /// <returns><see cref="TodoTask"/></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TodoTask>>> GetAllTodos()
    {
        // todo get guid from jwt
        Guid userId = new();
        var todos = await _mediator.Send(new GetTasksQuery(userId));
        return Ok(todos);
    }

 
    /// <summary>
    /// Get user's task by id/
    /// </summary>
    /// <param name="id">Task id</param>
    /// <returns><see cref="TodoTask"/></returns>
    [HttpGet, Route("{id}")]
    public async Task<ActionResult<TodoTask>> GetTodoById(Guid id)
    {
        // todo get guid from jwt
        Guid userId = new();
        var todoTask = await _mediator.Send(new GetTaskByIdQuery(userId, id));

        return todoTask != null ? Ok(todoTask) : NotFound();
    }

    /// <summary>
    /// Add new task to the user's list
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Created task</returns>
    [HttpPost]
    public async Task<ActionResult> CreateTask([FromBody] NewTaskDto request)
    {
        // todo get guid from jwt
        Guid userId = new();
        var addedTask = await _mediator.Send(new AddTaskCommand(userId, request));

        return CreatedAtRoute(nameof(GetTodoById), new { id = addedTask.Id }, addedTask);
    }

    [HttpPost, Route("fill_db")]
    public async Task FillDb(AppDbContext context) => await context.FillWithData();
}