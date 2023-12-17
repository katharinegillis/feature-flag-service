using Repositories = Infrastructure.Persistence.Repositories;

namespace WebAPI.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFeatureFlagRepository, Repositories.DbFeatureFlagRepository>();
    }
}