namespace TodoWebApi.Models;

public class TodoTask()
{
    public required string Title { get; set; }
    public required string Description { get; set; }
    public bool IsDone { get;  set; }
}
