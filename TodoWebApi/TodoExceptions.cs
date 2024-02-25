namespace TodoWebApi;

public class UserNotFoundException() : Exception("User not found");

public class ListNotFoundException() : Exception("List not found");

public class TaskNotFoundException() : Exception("Task not found");
