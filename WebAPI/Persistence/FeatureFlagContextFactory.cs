using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebAPI.Persistence;

// ReSharper disable once UnusedType.Global
public sealed class FeatureFlagContextFactory : IDesignTimeDbContextFactory<FeatureFlagContext>
{
    public FeatureFlagContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<FeatureFlagContext>();
        optionsBuilder.UseSqlite(configuration.GetConnectionString("FeatureFlagContext"),
            b => b.MigrationsAssembly("WebAPI"));

        return new FeatureFlagContext(optionsBuilder.Options);
    }
}

// Create migration: dotnet ef migrations add InitialCreate --project WebAPI/appsettings.json