using Microsoft.EntityFrameworkCore;
using Streakly.Core.Abstractions;
using Streakly.Infrastructure.DAL;

namespace Streakly.Infrastructure.Hangfire.Jobs;

public sealed class RevokedTokensCleanupJob(StreaklyDbContext dbContext, IClock clock)
{
    public async Task ExecuteAsync()
    {
        var now = clock.CurrentTimeUtc();

        await dbContext.RevokedTokens
            .Where(token => token.ExpiresAtUtc <= now)
            .ExecuteDeleteAsync();
    }
}