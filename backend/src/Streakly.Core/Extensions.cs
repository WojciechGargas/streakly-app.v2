using Microsoft.Extensions.DependencyInjection;
using Streakly.Core.DomainServices;
using Streakly.Core.DomainServices.Interfaces;
using Streakly.Core.Policies;
using Streakly.Core.Policies.Interfaces;

namespace Streakly.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddSingleton<IUserUpdatePolicy, UserUpdatePolicy>();

        return services;
    }
}