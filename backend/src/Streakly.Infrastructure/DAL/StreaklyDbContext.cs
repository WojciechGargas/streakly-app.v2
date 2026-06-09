using Microsoft.EntityFrameworkCore;
using Streakly.Core.Entities;
using Streakly.Infrastructure.DAL.Entities;

namespace Streakly.Infrastructure.DAL;

public class StreaklyDbContext(DbContextOptions<StreaklyDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<RevokedToken> RevokedTokens => Set<RevokedToken>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}