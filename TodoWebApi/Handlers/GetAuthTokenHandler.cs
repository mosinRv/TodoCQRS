using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TodoWebApi.Auth;
using TodoWebApi.Db;
using TodoWebApi.DTOs;
using TodoWebApi.Queries;

namespace TodoWebApi.Handlers;

public class GetAuthTokenHandler : IRequestHandler<GetAuthTokenQuery, LoginResult>
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _config;

    public GetAuthTokenHandler(AppDbContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }

    public async Task<LoginResult> Handle(GetAuthTokenQuery request, CancellationToken ctx)
    {
        var user =await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.NickName == request.Login.NickName, ctx);
        if (user == null || request.Login.PasswordHash != user.PasswordHash)
            return new LoginResult(string.Empty, false);

        var token = GenerateToken(user.Id.ToString());
        return new LoginResult(token, true);
    }

    private string GenerateToken(string identifier)
    {
        var secretKey = new SymmetricSecurityKey (Encoding.ASCII.GetBytes(
            _config.GetValue<string>(AuthConstants.SecretKeySectionName) ?? throw new InvalidOperationException()));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        
        var claims = new List<Claim> { new(ClaimTypes.NameIdentifier, identifier) };

        var token = new JwtSecurityToken(
            _config.GetValue<string>(AuthConstants.IssuerSectionName),
            _config.GetValue<string>(AuthConstants.AudienceSectionName),
            claims,
            DateTime.Now,
            DateTime.Now.AddMinutes(30),
            signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}