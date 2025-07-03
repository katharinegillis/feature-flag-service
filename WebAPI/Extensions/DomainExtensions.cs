using Domain.FeatureFlags;

namespace WebAPI.Extensions;

public static class DomainExtensions
{
    public static void AddDomainFactories(this IServiceCollection services)
    {
        services.AddScoped<IFactory, Factory>();
    }
}