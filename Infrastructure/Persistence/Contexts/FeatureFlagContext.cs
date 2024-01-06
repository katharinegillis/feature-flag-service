using EntityFramework.Exceptions.Sqlite;
using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class FeatureFlagContext : DbContext
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public virtual DbSet<FeatureFlag> FeatureFlags { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseExceptionProcessor();
    }

    public FeatureFlagContext(DbContextOptions<FeatureFlagContext> options) : base(options)
    {
    }

    public FeatureFlagContext()
    {
    }
}