using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class SqliteExtensions
{
    // ReSharper disable once UnusedMember.Global
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