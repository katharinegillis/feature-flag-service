using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class SqliteExtensions
{
    public static void AddSqliteServer(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<FeatureFlagContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("FeatureFlagContext")));
    }
}