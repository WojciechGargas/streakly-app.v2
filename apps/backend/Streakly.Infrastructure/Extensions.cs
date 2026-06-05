using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Streakly.Infrastructure.Auth;
using Streakly.Infrastructure.DAL;
using Streakly.Infrastructure.Exceptions;
using Streakly.Infrastructure.Security;

namespace Streakly.Infrastructure;

public static class Extensions
{
    private const string SectionName = "app";

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection(SectionName);
        var allowedOrigins = configuration.GetSection("cors:allowedOrigins").Get<string[]>() ?? [];

        var postgresOptions = configuration.GetOptions<PostgresOptions>("postgres");
        
        var infrastructureAssembly = typeof(AppOptions).Assembly;
        
        services.Configure<AppOptions>(section)
            .AddScoped<ExceptionMiddleware>()
            .AddSecurity()
            .AddAuth(configuration)
            .AddPostgres(configuration)
            .AddHttpContextAccessor()
            .AddSwaggerGen()
            .AddEndpointsApiExplorer();
        
        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseAuthentication();
        app.UseAuthorization();
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