using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class FeatureFlagContext : DbContext
{
    public DbSet<FeatureFlag> FeatureFlags { get; set; }

    public FeatureFlagContext(DbContextOptions<FeatureFlagContext> options) : base(options)
    {
    }
}