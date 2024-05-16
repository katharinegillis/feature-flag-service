namespace WebAPI.Extensions;

public static class DomainExtensions
{
    public static void AddDomainFactories(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
    }
}