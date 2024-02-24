using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace TodoWebApi.Auth;

public static class AuthExtensions
{
    public static void AddMyJwtAuthentication(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthentication().AddJwtBearer(opts =>
        {
            opts.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration.GetValue<string>(AuthConstants.IssuerSectionName),
                ValidAudience = builder.Configuration.GetValue<string>(AuthConstants.AudienceSectionName),
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                    builder.Configuration.GetValue<string>(AuthConstants.SecretKeySectionName) ?? throw new InvalidOperationException()))
            };
        });
    }

    /// <summary>
    /// Enable "Secure By Default"
    /// </summary>
    /// <param name="builder"></param>
    public static void EnableSecureByDefault(this WebApplicationBuilder builder)
    {
        builder.Services.AddAuthorization(opts =>
        {
            opts.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
        });
    }
}