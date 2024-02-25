using TaskStatus = TodoWebApi.Db.TaskStatus;

namespace TodoWebApi.DTOs;

public record UpdateTaskDto(Guid TaskId, TaskStatus UpdatedStatus);