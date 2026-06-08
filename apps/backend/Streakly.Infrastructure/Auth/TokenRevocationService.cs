using Microsoft.EntityFrameworkCore;
using Streakly.Application.Security;
using Streakly.Core.Abstractions;
using Streakly.Infrastructure.DAL;

namespace Streakly.Infrastructure.Auth;

internal sealed class TokenRevocationService(
    StreaklyDbContext dbContext,
    IClock clock) 
    : ITokenRevocationService
{
    public async Task RevokeTokenAsync(string tokenId, Guid userId, DateTime expiresAtUtc)
    {
        if (await dbContext.RevokedTokens.AnyAsync(x => x.TokenId == tokenId))
            return;

        var revokedAtUtc = clock.CurrentTimeUtc();
        
        await dbContext.RevokedTokens.AddAsync(new RevokedToken(tokenId, userId, expiresAtUtc, revokedAtUtc));
    }

    public Task<bool> IsTokenRevokedAsync(string tokenId)
        => dbContext.RevokedTokens.AnyAsync(x => x.TokenId == tokenId);
}