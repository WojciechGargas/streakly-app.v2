using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Streakly.Application.Security;
using Streakly.Core.Abstractions;
using Streakly.Core.Entities;
using Streakly.Core.Enums;
using Streakly.Core.ValueObjects;
using Streakly.Infrastructure.DAL;

namespace Streakly.Infrastructure.Seed;

internal sealed class AdminSeederHostedService(IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = serviceProvider.CreateScope();
        
        var options = scope.ServiceProvider
            .GetRequiredService<IOptions<AdminSeedOptions>>()
            .Value;

        if (!options.Enabled)
            return;
        
        var dbContext = scope.ServiceProvider.GetRequiredService<StreaklyDbContext>();
        var passwordManager = scope.ServiceProvider.GetRequiredService<IPasswordManager>();
        var clock = scope.ServiceProvider.GetRequiredService<IClock>();
        
        var email = new Email(options.Email);
        
        var adminExists = await dbContext.Users
            .AnyAsync(x => x.Email == email, cancellationToken);

        if (adminExists)
            return;
        
        var securedPassword = passwordManager.Secure(options.Password);
        
        var admin = new User(
            new UserId(Guid.NewGuid()),
            email,
            new Username(options.UserName),
            new Password(securedPassword),
            new Fullname(options.FullName),
            UserRole.Admin,
            clock.CurrentTimeUtc(),
            null,
            isEmailConfirmed: true);
        
        await dbContext.Users.AddAsync(admin, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}