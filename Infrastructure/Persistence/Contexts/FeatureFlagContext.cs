using Infrastructure.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Contexts;

public class FeatureFlagContext(DbContextOptions<FeatureFlagContext> options) : DbContext(options)
{
    public DbSet<FeatureFlag> FeatureFlags { get; set; } = null!;
}