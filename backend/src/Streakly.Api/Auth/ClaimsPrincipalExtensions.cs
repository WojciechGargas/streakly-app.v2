using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Streakly.Api.Auth;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserIdOrThrow(this ClaimsPrincipal user)
    {
        var raw = user.FindFirstValue(ClaimTypes.NameIdentifier)
                  ?? user.FindFirstValue(JwtRegisteredClaimNames.Sub)
                  ?? throw new UnauthorizedAccessException("Missing user id claim.");

        return !Guid.TryParse(raw, out var userId) ?
            throw new UnauthorizedAccessException("Invalid user id claim.") : userId;
    }

    public static string GetTokenIdOrThrow(this ClaimsPrincipal user)
    {
        return user.FindFirstValue(JwtRegisteredClaimNames.Jti)
            ?? throw new UnauthorizedAccessException("Missing token id claim.");
    }

    public static DateTime GetExpirationUtcOrThrow(this ClaimsPrincipal user)
    {
        var raw = user.FindFirstValue(JwtRegisteredClaimNames.Exp)
            ?? throw new UnauthorizedAccessException("Missing token expiration claim.");
        
        return !long.TryParse(raw, out var unixSeconds) ? 
            throw new UnauthorizedAccessException("Invalid token expiration claim.")
            : DateTimeOffset.FromUnixTimeSeconds(unixSeconds).UtcDateTime;
    }
}