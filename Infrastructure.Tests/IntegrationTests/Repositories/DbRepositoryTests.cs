using Domain.Common;
using EntityFramework.Exceptions.Sqlite;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.IntegrationTests.Repositories;

[NonParallelizable]
public sealed class DbRepositoryTests : BaseFeatureFlagContextRepositoryTests
{
    private SqliteConnection _connection = null!;
    private DbContextOptions<FeatureFlagContext> _options = null!;

    [SetUp]
    public void SetUp()
    {
        _connection = new SqliteConnection("Data Source=:memory:");
        _connection.Open();

        _options = new DbContextOptionsBuilder<FeatureFlagContext>()
            .UseSqlite(_connection)
            .UseExceptionProcessor()
            .Options;

        using (var context = new FeatureFlagContext(_options))
        {
            context.Database.EnsureCreated();
        }
    }

    [TearDown]
    public void TearDown()
    {
        _connection.Close();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_A_FeatureFlag()
    {
        await using var seedContext = new FeatureFlagContext(_options);
        seedContext.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });
        seedContext.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = false
        });
        await seedContext.SaveChangesAsync();

        await using var context = new FeatureFlagContext(_options);

        var repository = new DbRepository(context);

        var comparer = new Domain.FeatureFlags.EqualityComparer();

        var featureFlag = await repository.Get("some_flag");

        Assert.That(comparer.Equals(featureFlag, new Domain.FeatureFlags.Model
        {
            Id = "some_flag",
            Enabled = true
        }), Is.True);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_FeatureFlagNull_If_Not_Found()
    {
        await using var seedContext = new FeatureFlagContext(_options);

        seedContext.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = true
        });
        await seedContext.SaveChangesAsync();

        await using var context = new FeatureFlagContext(_options);

        var repository = new DbRepository(context);

        var featureFlag = await repository.Get("some_flag");

        Assert.That(featureFlag, Is.TypeOf<Domain.FeatureFlags.NullModel>());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_New_Flag_Id_And_Add_To_Context()
    {
        await using var context = new FeatureFlagContext(_options);

        var repository = new DbRepository(context);

        var featureFlag = new Domain.FeatureFlags.Model
        {
            Id = "new_flag",
            Enabled = true
        };

        var result = await repository.Create(featureFlag);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.True);
            Assert.That(result.Value, Is.EqualTo("new_flag"));
        });
    }

    [Test]
    public async Task
        DbFeatureFlagRepository_Create_Should_Return_Validation_Error_If_Id_Already_Exists()
    {
        await using var seedContext = new FeatureFlagContext(_options);

        var featureFlag = new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        };
        seedContext.FeatureFlags.Add(featureFlag);
        await seedContext.SaveChangesAsync();

        await using var context = new FeatureFlagContext(_options);

        var repository = new DbRepository(context);

        var result = await repository.Create(new Domain.FeatureFlags.Model
        {
            Id = "some_flag",
            Enabled = false
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.InstanceOf<ValidationError>());

            var validationError = result.Error as ValidationError;
            Assert.That(validationError?.Field, Is.EqualTo("Id"));
            Assert.That(validationError?.Message, Is.EqualTo("Already exists"));
        });
    }
}