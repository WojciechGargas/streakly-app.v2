namespace Streakly.Infrastructure.Seed;

internal sealed class AdminSeedOptions
{
    public const string SectionName = "adminSeed";

    public bool Enabled { get; init; }
    public string Email { get; init; } = string.Empty;
    public string UserName { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string FullName { get; init; } = string.Empty;
}