using Microsoft.Extensions.DependencyInjection;
using Repositories = Infrastructure.Persistence.Repositories;

namespace Console.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
        services.AddScoped<Domain.FeatureFlags.IRepository, Repositories.DbFeatureFlagRepository>();
        services.AddScoped<Domain.FeatureFlags.IReadRepository, Repositories.DbFeatureFlagRepository>();
    }
}