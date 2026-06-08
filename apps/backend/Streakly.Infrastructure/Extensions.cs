using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Streakly.Core.Abstractions;
using Streakly.Infrastructure.Auth;
using Streakly.Infrastructure.DAL;
using Streakly.Infrastructure.Emails;
using Streakly.Infrastructure.Exceptions;
using Streakly.Infrastructure.Hangfire;
using Streakly.Infrastructure.Hangfire.Jobs;
using Streakly.Infrastructure.Hangfire.Jobs.Options;
using Streakly.Infrastructure.Security;
using Streakly.Infrastructure.Seed;
using Streakly.Infrastructure.Time;

namespace Streakly.Infrastructure;

public static class Extensions
{
    private const string SectionName = "app";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        var allowedOrigins = configuration.GetSection("cors:allowedOrigins").Get<string[]>() ?? [];

        var postgresOptions = configuration.GetOptions<PostgresOptions>("postgres");

        services.Configure<HangfireOptions>(configuration.GetSection(HangfireOptions.SectionName));
        services.Configure<UserCleanupOptions>(configuration.GetSection(UserCleanupOptions.SectionName));
        services.Configure<AdminSeedOptions>(configuration.GetSection(AdminSeedOptions.SectionName));
        
        services.Configure<AppOptions>(section)
            .AddScoped<ExceptionMiddleware>()
            .AddSecurity()
            .AddAuth(configuration)
            .AddEmails(configuration)
            .AddPostgres(configuration)
            .AddHangfire(config =>
            {
                config.UsePostgreSqlStorage(storageOptions =>
                    storageOptions.UseNpgsqlConnection(postgresOptions.ConnectionString));
            })
            .AddHangfireServer()
            .AddHostedService<AdminSeederHostedService>()
            .AddHttpContextAccessor()
            .AddSwaggerGen()
            .AddEndpointsApiExplorer()
            .AddSingleton<IClock, Clock>();

        services.AddCors(options =>
        {
            options.AddPolicy("cors", policy =>
            {
                policy
                    .WithOrigins(allowedOrigins)
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        var hangfireOptions = app.Services
            .GetRequiredService<Microsoft.Extensions.Options.IOptions<HangfireOptions>>()
            .Value;
        
        var userCleanupOptions = app.Services
            .GetRequiredService<Microsoft.Extensions.Options.IOptions<UserCleanupOptions>>()
            .Value;
        
        var recurringJobManager = app.Services.GetRequiredService<IRecurringJobManager>();
        
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseCors("cors");
        app.UseAuthentication();
        app.UseAuthorization();
        recurringJobManager.AddOrUpdate<RevokedTokensCleanupJob>(
            "cleanup-revoked-tokens",
            job => job.ExecuteAsync(),
            hangfireOptions.CleanupCron);
        recurringJobManager.AddOrUpdate<UnconfirmedUsersCleanupJob>(
            "cleanup-unconfirmed-users",
            job => job.ExecuteAsync(),
            userCleanupOptions.CleanupCron);
        if (hangfireOptions.DashboardEnabled)
        {
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = [new HangfireDashboardAuthorizationFilter()]
            });
        }
        app.MapControllers();
        
        return app;
    }
    
    public static T GetOptions<T>(this IConfiguration configuration, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = configuration.GetSection(sectionName);
        section.Bind(options);

        return options;
    }
}