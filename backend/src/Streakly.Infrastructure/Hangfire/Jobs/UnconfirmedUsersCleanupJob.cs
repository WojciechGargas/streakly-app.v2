using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Streakly.Core.Abstractions;
using Streakly.Core.Enums;
using Streakly.Infrastructure.DAL;
using Streakly.Infrastructure.Hangfire.Jobs.Options;

namespace Streakly.Infrastructure.Hangfire.Jobs;

public sealed class UnconfirmedUsersCleanupJob(
    StreaklyDbContext dbContext,
    IClock clock,
    IOptions<UserCleanupOptions> options)
{
    public async Task ExecuteAsync()
    {
        var deleteBeforeUtc = clock.CurrentTimeUtc()
            .AddDays(-options.Value.UnconfirmedDeleteAfterDays);

        await dbContext.Users
            .Where(user => !user.IsEmailConfirmed)
            .Where(user => user.CreatedAt <= deleteBeforeUtc)
            .ExecuteDeleteAsync();
    }
}