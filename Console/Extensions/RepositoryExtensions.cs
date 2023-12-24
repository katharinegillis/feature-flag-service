using Microsoft.Extensions.DependencyInjection;
using Repositories = Infrastructure.Persistence.Repositories;

namespace Console.Extensions;

public static class RepositoryExtensions
{
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IRepository, Repositories.DbRepository>();
    }
}