using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TodoWebApi.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _config;

    public AuthController(IConfiguration config)
    {
        _config = config;
    }

    [HttpGet]
    public IActionResult Info()
    {
        return Ok("info");
    }

    [HttpPost("LogIn")]
    public IActionResult LogIn(string username)
    {
        bool isValidUser = true;
        if (!isValidUser)
        {
            return BadRequest("invalid");
        }

        var token = GenerateToken(username);

        Response.Cookies.Append("Authorization",  "Bearer "+ token, new CookieOptions
        {
            HttpOnly = true,
            Secure = true,
            SameSite = SameSiteMode.Strict,
            Expires = DateTime.Now.AddMinutes(30)
        });
        
        return Ok(new
        {
            token
        });
    }

    private string GenerateToken(string username)
    {
        var secretKey = new (Encoding.ASCII.GetBytes(
            _config.GetValue<string>("Authentication:SecretKey") ?? throw new InvalidOperationException()));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim> { new(ClaimTypes.Name, username) };

        var token = new JwtSecurityToken(
            _config.GetValue<string>("Authentication:Issuer"),
            _config.GetValue<string>("Authentication:Audience"),
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(30),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}