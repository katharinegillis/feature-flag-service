using Repositories = Infrastructure.Persistence.Repositories;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class RepositoryExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IRepository, Repositories.DbFeatureFlagRepository>();
    }
}