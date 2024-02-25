using System.Security.Claims;
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
    private readonly IMediator _mediator;

    public TodoController(IMediator mediator)
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
        if (TryExtractUserId(out var userId)) return BadRequest(NoUserIdReqResultMsg);

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
        if (TryExtractUserId(out var userId)) return BadRequest(NoUserIdReqResultMsg);

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
        if (TryExtractUserId(out var userId)) return BadRequest(NoUserIdReqResultMsg);

        var addedTask = await _mediator.Send(new AddTaskCommand(userId, request));

        return CreatedAtRoute(nameof(GetTodoById), new { id = addedTask.Id }, addedTask);
    }

    [HttpPost, Route("list")]
    public async Task<ActionResult> CreateTaskList([FromBody] NewListDto request)
    {
        if (TryExtractUserId(out var userId)) return BadRequest(NoUserIdReqResultMsg);
        var addedList = await _mediator.Send(new AddListCommand(userId, request));

        return Created(addedList.Id.ToString(), addedList);
    }

    /// <summary>
    /// Extracting userId from JWT 
    /// </summary>
    /// <param name="userId">special guid for user</param>
    /// <returns>true if the extracting was successful; otherwise, false</returns>
    private bool TryExtractUserId(out Guid userId)
    {
        var userIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        return Guid.TryParse(userIdStr, out userId);
    }

    private const string NoUserIdReqResultMsg = "Can't find userId in JWT";
}