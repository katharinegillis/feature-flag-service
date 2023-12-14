using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.IntegrationTests.Repositories;

public class DbFeatureFlagRepositoryTests
{
    private FeatureFlagContext? _context;

    [SetUp]
    public void SetUp()
    {
        var options = new DbContextOptionsBuilder<FeatureFlagContext>()
            .UseInMemoryDatabase(databaseName: "DbFeatureFlagRepositoryTestDb")
            .Options;

        _context = new FeatureFlagContext(options);
        _context.Database.EnsureDeleted();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_A_FeatureFlag()
    {
        if (_context is null)
        {
            Assert.Fail("Database context is null");
            return;
        }

        _context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });
        _context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = false
        });
        await _context.SaveChangesAsync();

        var repository = new DbFeatureFlagRepository(_context);

        var comparer = new Domain.FeatureFlags.FeatureFlagEqualityComparer();

        var featureFlag = await repository.Get("some_flag");

        Assert.That(featureFlag, Is.TypeOf<Domain.FeatureFlags.FeatureFlag>());
        Assert.That(comparer.Equals(featureFlag, new Domain.FeatureFlags.FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        }), Is.True);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_FeatureFlagNull_If_Not_Found()
    {
        if (_context is null)
        {
            Assert.Fail("Database context is null");
            return;
        }

        _context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = true
        });
        await _context.SaveChangesAsync();

        var repository = new DbFeatureFlagRepository(_context);

        var featureFlag = await repository.Get("some_flag");

        Assert.That(featureFlag, Is.TypeOf<Domain.FeatureFlags.FeatureFlagNull>());
    }
}