namespace Streakly.Infrastructure.Hangfire.Jobs.Options;

public sealed class UserCleanupOptions
{
    public const string SectionName = "userCleanup";
    
    public int UnconfirmedDeleteAfterDays { get; init; } = 7;
    public string CleanupCron { get; init; } = "0 3 * * *";
}