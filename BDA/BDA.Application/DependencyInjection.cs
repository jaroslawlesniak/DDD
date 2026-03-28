using Microsoft.Extensions.DependencyInjection;

using BDA.Application.Services.Authentication;

namespace BDA.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }
}