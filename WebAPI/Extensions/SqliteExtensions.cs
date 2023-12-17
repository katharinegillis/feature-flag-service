using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

public static class SqliteExtensions
{
    public static void AddSqliteServer(this IServiceCollection services)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<FeatureFlagContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("FeatureFlagContext")));
    }
}