using Domain.Common;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.IntegrationTests.Repositories;

public class DbFeatureFlagRepositoryTests : BaseFeatureFlagContextRepositoryTests
{
    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_A_FeatureFlag()
    {
        await using var connection = GetSqliteConnection();
        var options = GetSqliteContextOptions(connection);

        await using var context = new FeatureFlagContext(options);
        context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });
        context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = false
        });
        await context.SaveChangesAsync();

        var repository = new DbFeatureFlagRepository(context);

        var comparer = new Domain.FeatureFlags.FeatureFlagEqualityComparer();

        var featureFlag = await repository.Get("some_flag");

        Assert.That(comparer.Equals(featureFlag, new Domain.FeatureFlags.FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        }), Is.True);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_FeatureFlagNull_If_Not_Found()
    {
        await using var connection = GetSqliteConnection();
        var options = GetSqliteContextOptions(connection);

        await using var context = new FeatureFlagContext(options);

        context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_other_flag",
            Enabled = true
        });
        await context.SaveChangesAsync();

        var repository = new DbFeatureFlagRepository(context);

        var featureFlag = await repository.Get("some_flag");

        Assert.That(featureFlag, Is.TypeOf<Domain.FeatureFlags.FeatureFlagNull>());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_New_Flag_Id_And_Add_To_Context()
    {
        await using var connection = GetSqliteConnection();
        var options = GetSqliteContextOptions(connection);

        await using var context = new FeatureFlagContext(options);

        var repository = new DbFeatureFlagRepository(context);

        var featureFlag = new Domain.FeatureFlags.FeatureFlag
        {
            Id = "new_flag",
            Enabled = true
        };

        var result = await repository.Create(featureFlag);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.True);
            Assert.That(result.Value, Is.EqualTo("new_flag"));

            Assert.That(context.FeatureFlags.Count, Is.EqualTo(1));
            Assert.That(context.FeatureFlags.First(), Is.EqualTo(new FeatureFlag
            {
                FeatureFlagId = "new_flag",
                Enabled = true
            }));
        });
    }

    [Test]
    public async Task
        DbFeatureFlagRepository_Create_Should_Return_Validation_Error_If_Id_Already_Exists()
    {
        await using var connection = GetSqliteConnection();
        var options = GetSqliteContextOptions(connection);

        await using var context = new FeatureFlagContext(options);

        context.FeatureFlags.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });
        await context.SaveChangesAsync();

        var repository = new DbFeatureFlagRepository(context);

        var result = await repository.Create(new Domain.FeatureFlags.FeatureFlag
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
            Assert.That(validationError?.Message, Is.EqualTo("Id already exists"));

            Assert.That(context.FeatureFlags.Count, Is.EqualTo(1));
            Assert.That(context.FeatureFlags.First(), Is.EqualTo(new FeatureFlag
            {
                FeatureFlagId = "some_flag",
                Enabled = true
            }));
        });
    }
}