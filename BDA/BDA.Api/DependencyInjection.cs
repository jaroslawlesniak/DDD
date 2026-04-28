using BDA.Api.Common.Errors;
using BDA.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace BDA.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, BdaProblemDetailsFactory>();
        services.AddMappings();

        return services;
    }
}