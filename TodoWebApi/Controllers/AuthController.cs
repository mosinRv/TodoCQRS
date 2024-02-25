using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoWebApi.Db;
using TodoWebApi.DTOs;
using TodoWebApi.Queries;

namespace TodoWebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Test authentication
    /// </summary>
    /// <returns>userId if jwt contains it </returns>
    [HttpGet]
    [Authorize]
    public IActionResult Test()
    {
        var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        return Ok(user ?? "Err");
    }

    [HttpPost("LogIn")]
    public async Task<IActionResult> LogIn(LogInRequest request)
    {
        var loginResult = await _mediator.Send(new GetAuthTokenQuery(request));
        if(!loginResult.IsSucceed) return BadRequest("Invalid");

        return Ok(new { Token = loginResult.Token });
    }
    
    [HttpPost, Route("fill_db")]
    public async Task FillDb(AppDbContext context) => await context.FillWithData();
}