using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace WebAPI.Persistence;

public class FeatureFlagContextFactory : IDesignTimeDbContextFactory<FeatureFlagContext>
{
    public FeatureFlagContext CreateDbContext(string[] args)
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
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