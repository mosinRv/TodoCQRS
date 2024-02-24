using MediatR;
using TodoWebApi.DTOs;

namespace TodoWebApi.Queries;

public record GetAuthTokenQuery(LogInRequest Login) : IRequest<LoginResult>;