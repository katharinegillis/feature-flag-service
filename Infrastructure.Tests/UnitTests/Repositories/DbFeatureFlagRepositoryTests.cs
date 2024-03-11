using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Tests.Common;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.UnitTests.Repositories;

[Category("Unit")]
public sealed class DbFeatureFlagRepositoryTests
{
    [Test]
    public async Task DbFeatureFlagRepository_Get_Should_Return_A_Feature_Flag()
    {
        var data = new List<FeatureFlag>
        {
            new()
            {
                FeatureFlagId = "some_flag",
                Enabled = true
            }
        };

        var setMock = TestingUtils.CreateDbSetMockFromList(data);

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("some_flag", true)).Returns(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

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
        var setMock = TestingUtils.CreateDbSetMockFromList(new List<FeatureFlag>());

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create()).Returns(NullModel.Instance);

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Get("some_flag");

        Assert.That(result, Is.InstanceOf<NullModel>());
        Assert.That(result.IsNull);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Id()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();
        setMock.Setup(m => m.Add(It.IsAny<FeatureFlag>()));

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factory = Mock.Of<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factory);

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

        setMock.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Validation_Error_If_Already_Exists()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(m => m.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(m => m.SaveChanges()).Callback(() => throw new UniqueConstraintException());

        var factory = Mock.Of<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factory);

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

        setMock.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Returns_Error_If_Unknown_Error_Occurs()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(m => m.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(m => m.SaveChanges())
            .Callback(() => throw new Exception("Unknown error"));

        var factory = Mock.Of<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factory);

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

        setMock.Verify(m => m.Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        }), Times.Once());

        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_List_Should_Return_List_Of_FeatureFlags()
    {
        var data = new List<FeatureFlag>
        {
            new()
            {
                FeatureFlagId = "some_flag",
                Enabled = true
            },
            new()
            {
                FeatureFlagId = "another_flag",
                Enabled = false
            }
        };

        var setMock = TestingUtils.CreateDbSetMockFromList(data);

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factoryMock = new Mock<IFactory>();
        factoryMock.Setup(f => f.Create("some_flag", true)).Returns(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        factoryMock.Setup(f => f.Create("another_flag", false)).Returns(new Model
        {
            Id = "another_flag",
            Enabled = false
        });

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.List();

        var comparer = new EqualityComparer();
        var list = result.ToList();
        Assert.Multiple(() =>
        {
            Assert.That(comparer.Equals(list[0], new Model
            {
                Id = "some_flag",
                Enabled = true
            }));
            Assert.That(comparer.Equals(list[1], new Model
            {
                Id = "another_flag",
                Enabled = false
            }));
        });
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_True()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();
        setMock.Setup(m => m.Update(It.IsAny<FeatureFlag>()));

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value);
        });

        setMock.Verify(m => m.Update(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = false
        }), Times.Once);

        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_NotFoundError()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(m => m.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(m => m.SaveChanges()).Callback(() => throw new DbUpdateConcurrencyException());

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new NotFoundError
            {
                Message = "Not found"
            }));
        });

        setMock.Verify(m => m.Update(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = false
        }));

        contextMock.Verify(m => m.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_Error_If_Error_Occurs()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();
        setMock.Setup(s => s.Update(It.IsAny<FeatureFlag>()));

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Callback(() => throw new Exception("Unknown error"));

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        });
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_True()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();
        setMock.Setup(m => m.Remove(It.IsAny<FeatureFlag>()));

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Delete("some_flag");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value);
        });

        setMock.Verify(m => m.Remove(new FeatureFlag
        {
            FeatureFlagId = "some_flag"
        }), Times.Once);

        contextMock.Verify(c => c.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_NotFoundError_If_FeatureFlag_Not_Found()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Callback(() => throw new DbUpdateConcurrencyException());

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Delete("some_flag");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new NotFoundError()));
        });

        setMock.Verify(m => m.Remove(new FeatureFlag
        {
            FeatureFlagId = "some_flag"
        }), Times.Once());

        contextMock.Verify(c => c.SaveChanges(), Times.Once());
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_Error_If_Error_Occurs()
    {
        var setMock = new Mock<DbSet<FeatureFlag>>();
        setMock.Setup(s => s.Remove(It.IsAny<FeatureFlag>()));

        var contextMock = new Mock<FeatureFlagContext>();
        contextMock.Setup(c => c.FeatureFlags).Returns(setMock.Object);
        contextMock.Setup(c => c.SaveChanges()).Callback(() => throw new Exception("Unknown error"));

        var factoryMock = new Mock<IFactory>();

        var repository = new DbFeatureFlagRepository(contextMock.Object, factoryMock.Object);

        var result = await repository.Delete("some_flag");

        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        });
    }
}