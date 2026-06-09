using Hangfire.Dashboard;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Streakly.Infrastructure.Hangfire;

internal sealed class HangfireDashboardAuthorizationFilter : IDashboardAuthorizationFilter
{
    public bool Authorize(DashboardContext context)
    {
        var httpContext = context.GetHttpContext();

        var environment = httpContext.RequestServices
            .GetRequiredService<IWebHostEnvironment>();
        
        if (environment.IsDevelopment())
        {
            return true; // Skip dashboard authorization in development only.
        }

        return httpContext.User.Identity?.IsAuthenticated == true
               && httpContext.User.IsInRole("Admin");

    }
}