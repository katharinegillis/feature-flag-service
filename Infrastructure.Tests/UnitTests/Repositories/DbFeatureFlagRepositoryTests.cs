using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;

namespace Infrastructure.Tests.UnitTests.Repositories;

public class DbFeatureFlagRepositoryTests
{
    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_A_Feature_Flag()
    {
        var data = new List<FeatureFlag>
        {
            new()
            {
                FeatureFlagId = "some_flag",
                Enabled = true,
            }
        };

        var mockSet = TestingUtils.CreateDbSetMockFromList(data);

        var mockContext = new Mock<FeatureFlagContext>();
        mockContext.Setup(c => c.FeatureFlags).Returns(mockSet.Object);

        var repository = new DbFeatureFlagRepository(mockContext.Object);

        var result = await repository.Get("some_flag");

        var comparer = new EqualityComparer();
        Assert.That(comparer.Equals(result, new Model
        {
            Id = "some_flag",
            Enabled = true
        }));
    }

    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_NullModel_If_Not_Found()
    {
        var data = new List<FeatureFlag>();

        var mockSet = TestingUtils.CreateDbSetMockFromList(data);

        var mockContext = new Mock<FeatureFlagContext>();
        mockContext.Setup(c => c.FeatureFlags).Returns(mockSet.Object);

        var repository = new DbFeatureFlagRepository(mockContext.Object);

        var result = await repository.Get("some_flag");

        Assert.That(result, Is.InstanceOf<NullModel>());
        Assert.That(result.IsNull);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Id()
    {
        var mockSet = new Mock<DbSet<FeatureFlag>>();
        mockSet.Setup(m => m.Add(It.IsAny<FeatureFlag>()));

        var mockContext = new Mock<FeatureFlagContext>();
        mockContext.Setup(c => c.FeatureFlags).Returns(mockSet.Object);

        var repository = new DbFeatureFlagRepository(mockContext.Object);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value, Is.EqualTo("some_flag"));
        });

        mockSet.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Validation_Error_If_Alredy_Exists()
    {
        var mockSet = new Mock<DbSet<FeatureFlag>>();

        var mockContext = new Mock<FeatureFlagContext>();
        mockContext.Setup(m => m.FeatureFlags).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChanges()).Callback(() => throw new UniqueConstraintException());

        var repository = new DbFeatureFlagRepository(mockContext.Object);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new ValidationError
            {
                Field = "Id",
                Message = "Already exists"
            }));
        });

        mockSet.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Returns_Error_If_Unknown_Error_Occurs()
    {
        var mockSet = new Mock<DbSet<FeatureFlag>>();

        var mockContext = new Mock<FeatureFlagContext>();
        mockContext.Setup(m => m.FeatureFlags).Returns(mockSet.Object);
        mockContext.Setup(m => m.SaveChanges())
            .Callback(() => throw new Exception("Unknown error"));

        var repository = new DbFeatureFlagRepository(mockContext.Object);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        });

        mockSet.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        mockContext.Verify(m => m.SaveChanges(), Times.Once());
    }
}