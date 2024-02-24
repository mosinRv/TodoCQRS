namespace TodoWebApi.DTOs;

public record LogInRequest(string NickName, string PasswordHash);

public record LoginResult(string Token, bool IsSucceed);

