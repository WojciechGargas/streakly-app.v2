using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Streakly.Application.Security;

namespace Streakly.Infrastructure.Auth;

internal static class Extensions
{
    private const string SectionName = "auth";

    public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(SectionName));
        services.AddScoped<ITokenRevocationService, TokenRevocationService>();
        var options = configuration.GetOptions<AuthOptions>(SectionName);

        services
            .AddSingleton<IAuthenticator, Authenticator>()
            .AddAuthentication(a =>
            {
                a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Audience = options.Audience;
                x.IncludeErrorDetails = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = options.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                };

                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var tokenId = context.Principal?.FindFirstValue(JwtRegisteredClaimNames.Jti);

                        if (string.IsNullOrWhiteSpace(tokenId))
                        {
                            context.Fail("Token not found");
                            return;
                        }

                        var tokenRevocationService = context.HttpContext.RequestServices
                            .GetRequiredService<ITokenRevocationService>();

                        if (await tokenRevocationService.IsTokenRevokedAsync(tokenId))
                        {
                            context.Fail("Token has been revoked");
                        }
                    }
                };
            });
        
        return services;
    }
}