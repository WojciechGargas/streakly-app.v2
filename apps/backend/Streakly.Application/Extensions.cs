using Microsoft.Extensions.DependencyInjection;

namespace Streakly.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Extensions).Assembly);
        });

        return services;
    }
}
