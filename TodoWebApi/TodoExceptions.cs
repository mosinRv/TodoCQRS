namespace TodoWebApi;

public class UserNotFoundException : Exception
{
    public UserNotFoundException() : base("User not found") { }
}

public class ListNotFoundException : Exception
{
    public ListNotFoundException() : base("List not found") { }
}
