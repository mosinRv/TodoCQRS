
using FluentAssertions;
using TodoWebApi.Commands;
using TodoWebApi.Db;
using TodoWebApi.DTOs;
using TodoWebApi.Handlers;
using TodoWebApi.Queries;
using TaskStatus = TodoWebApi.Db.TaskStatus;

namespace TodoTesting;

public class UnitTest1
{
    private readonly AppDbContext _context;

    public UnitTest1()
    {
        _context = new TestSqLiteDbContext();
        _context.FillWithDataAsync().GetAwaiter().GetResult();
    }


    [Fact]
    public async Task GetTasks_Test()
    {
        var handler = new GetTasksHandler(_context);
        var userId = DbTestExtensions.DefaultUsers[0].Id;
        var query = new GetTasksQuery(userId);
        
        var result = (await handler.Handle(query, default)).ToArray();
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task CreateNewTask_Test()
    {
        var handler = new AddTaskHandler(_context);
        var userId = DbTestExtensions.DefaultUsers[0].Id;
        var query = new AddTaskCommand(userId, new NewTaskDto()
        {
            Title = "New cool task",
            Description = "Description",
            TaskListTitle = DbTestExtensions.DefaultUsers[0].Lists![0].Title,
            Status = TaskStatus.Waiting
        });
        
        var result = await handler.Handle(query, default);
        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task GetTaskById_Test()
    {
        var handler = new GetTaskByIdHandler(_context);
        var userId = DbTestExtensions.DefaultUsers[0].Id;
        var taskId = DbTestExtensions.DefaultUsers[0].Lists![0].TodoTasks![0].Id;
        var query = new GetTaskByIdQuery(userId, taskId);
        
        var result = await handler.Handle(query, default);
        result.Should().NotBeNull();
        result!.Id.Should().Be(taskId);
    }

    [Fact]
    public async Task CreateNewList_Test()
    {
        var handler = new AddListHandler(_context);
        var userId = DbTestExtensions.DefaultUsers[0].Id;
        var query = new AddListCommand(userId, new NewListDto()
        {
            Title = "New cool List",
            Description = "Description",
        });
        
        var result = await handler.Handle(query, default);
        result.Id.Should().NotBe(Guid.Empty);
    }

    [Fact]
    public async Task UpdateTaskStatus_Test()
    {
        var handler = new UpdateTaskHandler(_context);
        var userId = DbTestExtensions.DefaultUsers[0].Id;
        var taskId = DbTestExtensions.DefaultUsers[0].Lists![0].TodoTasks![0].Id;
        var query = new UpdateTaskStatusCommand(
            userId,
            new UpdateTaskDto(taskId, TaskStatus.Done));
        
        var result = await handler.Handle(query, default);
        result.Id.Should().NotBe(Guid.Empty);
        result.Status.Should().Be(TaskStatus.Done);
    }
}