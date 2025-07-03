using Domain.FeatureFlags;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class DomainExtensions
{
    public static void AddDomainFactories(this IServiceCollection services)
    {
        services.AddScoped<IFactory, Factory>();
    }
}