using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Models;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Tests.Common;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Infrastructure.Tests.Unit.Repositories;

[Parallelizable]
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

        var dbSet = TestingUtils.CreateDbSetMockFromList(data);

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(dbSet);

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", true).Returns(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        var repository = new DbFeatureFlagRepository(context, factory);

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
        var set = TestingUtils.CreateDbSetMockFromList(new List<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);

        var factory = Substitute.For<IFactory>();
        factory.Create().Returns(NullModel.Instance);

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Get("some_flag");

        Assert.That(result, Is.InstanceOf<NullModel>());
        Assert.That(result.IsNull);
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Id()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();
        set.Add(Arg.Any<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value, Is.EqualTo("some_flag"));
        }

        set.Received(1).Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Should_Return_Validation_Error_If_Already_Exists()
    {
        // var setMock = new Mock<DbSet<FeatureFlag>>();
        //
        // var contextMock = new Mock<FeatureFlagContext>();
        // contextMock.Setup(m => m.FeatureFlags).Returns(setMock.Object);
        // contextMock.Setup(m => m.SaveChanges()).Callback(() => throw new UniqueConstraintException());
        //
        // var factory = Mock.Of<IFactory>();
        var set = Substitute.For<DbSet<FeatureFlag>>();

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new UniqueConstraintException());

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new ValidationError
            {
                Field = "Id",
                Message = "Already exists"
            }));
        }

        set.Received(1).Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Create_Returns_Error_If_Unknown_Error_Occurs()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new Exception("Unknown error"));

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Create(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        }

        set.Received(1).Add(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = true
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
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

        var set = TestingUtils.CreateDbSetMockFromList(data);

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);

        var factory = Substitute.For<IFactory>();
        factory.Create("some_flag", true).Returns(new Model
        {
            Id = "some_flag",
            Enabled = true
        });
        factory.Create("another_flag", false).Returns(new Model
        {
            Id = "another_flag",
            Enabled = false
        });

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.List();

        var comparer = new EqualityComparer();
        var list = result.ToList();
        using (Assert.EnterMultipleScope())
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
        }
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_True()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();
        set.Update(Arg.Any<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value);
        }


        set.Received(1).Update(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = false
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_NotFoundError()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new DbUpdateConcurrencyException());

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new NotFoundError
            {
                Message = "Not found"
            }));
        }

        set.Received(1).Update(new FeatureFlag
        {
            FeatureFlagId = "some_flag",
            Enabled = false
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Update_Should_Return_Error_If_Error_Occurs()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();
        set.Update(Arg.Any<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new Exception("Unknown error"));

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Update(new Model
        {
            Id = "some_flag",
            Enabled = false
        });

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        }
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_True()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();
        set.Remove(Arg.Any<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Delete("some_flag");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk);
            Assert.That(result.Value);
        }

        set.Received(1).Remove(new FeatureFlag
        {
            FeatureFlagId = "some_flag"
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_NotFoundError_If_FeatureFlag_Not_Found()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new DbUpdateConcurrencyException());

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Delete("some_flag");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new NotFoundError()));
        }

        set.Received(1).Remove(new FeatureFlag
        {
            FeatureFlagId = "some_flag"
        });

        // ReSharper disable once MethodHasAsyncOverload
        context.Received(1).SaveChanges();
    }

    [Test]
    public async Task DbFeatureFlagRepository_Delete_Should_Return_Error_If_Error_Occurs()
    {
        var set = Substitute.For<DbSet<FeatureFlag>>();
        set.Remove(Arg.Any<FeatureFlag>());

        var context = Substitute.For<FeatureFlagContext>();
        context.FeatureFlags.Returns(set);
        // ReSharper disable once MethodHasAsyncOverload
        context.SaveChanges().Throws(new Exception("Unknown error"));

        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        var result = await repository.Delete("some_flag");

        using (Assert.EnterMultipleScope())
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error, Is.EqualTo(new Error
            {
                Message = "Unknown error"
            }));
        }
    }

    [Test]
    public void DbFeatureFlagRepository_Name_Should_Return_Database()
    {
        var context = Substitute.For<FeatureFlagContext>();
        var factory = Substitute.For<IFactory>();

        var repository = new DbFeatureFlagRepository(context, factory);

        Assert.That(repository.Name, Is.EqualTo("Database"));
    }
}