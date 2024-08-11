using Domain.FeatureFlags;
using Infrastructure.Configuration;
using Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Options;
using NSubstitute;
using Splitio.Services.Client.Interfaces;

namespace Infrastructure.Tests.Integration.Repositories;

public sealed class SplitIoFeatureFlagRepositoryTests
{
    [Test]
    public async Task SplitIoFeatureFlagRepository_Get_Should_Return_A_Treatment_On_As_Enabled_Feature_Flag()
    {
        var splitClient = Substitute.For<ISplitClient>();
        splitClient.GetTreatment("system_key", "some_flag").Returns("on");

        var splitFactory = Substitute.For<ISplitFactory>();
        splitFactory.Client().Returns(splitClient);

        var splitOptions = new SplitIoOptions
        {
            SdkKey = "test_key",
            TreatmentKey = "system_key"
        };

        var splitOptionsWrapper = Options.Create(splitOptions);

        var factory = new Factory();

        var repository = new SplitIoFeatureFlagRepository(splitFactory, splitOptionsWrapper, factory);

        var result = await repository.Get("some_flag");

        var comparer = new EqualityComparer();
        Assert.That(comparer.Equals(result, new Model
        {
            Id = "some_flag",
            Enabled = true
        }));
    }

    [Test]
    public async Task SplitIoFeatureFlagRepository_Get_Should_Return_A_Treatment_Off_As_Disabled_Feature_Flag()
    {
        var splitClient = Substitute.For<ISplitClient>();
        splitClient.GetTreatment("system_key", "some_flag").Returns("off");

        var splitFactory = Substitute.For<ISplitFactory>();
        splitFactory.Client().Returns(splitClient);

        var splitOptions = new SplitIoOptions
        {
            SdkKey = "test_key",
            TreatmentKey = "system_key"
        };

        var splitOptionsWrapper = Options.Create(splitOptions);

        var factory = new Factory();

        var repository = new SplitIoFeatureFlagRepository(splitFactory, splitOptionsWrapper, factory);

        var result = await repository.Get("some_flag");

        var comparer = new EqualityComparer();
        Assert.That(comparer.Equals(result, new Model
        {
            Id = "some_flag",
            Enabled = false
        }));
    }

    [Test]
    public async Task SplitIoFeatureFlagRepository_Get_Should_Return_A_Treatment_Control_As_Null_Feature_Flag()
    {
        var splitClient = Substitute.For<ISplitClient>();
        splitClient.GetTreatment("system_key", "some_flag").Returns("control");

        var splitFactory = Substitute.For<ISplitFactory>();
        splitFactory.Client().Returns(splitClient);

        var splitOptions = new SplitIoOptions
        {
            SdkKey = "test_key",
            TreatmentKey = "system_key"
        };

        var splitOptionsWrapper = Options.Create(splitOptions);

        var factory = new Factory();

        var repository = new SplitIoFeatureFlagRepository(splitFactory, splitOptionsWrapper, factory);

        var result = await repository.Get("some_flag");

        Assert.That(result.IsNull);
    }

    [Test]
    public async Task SplitIoFeatureFlagRepository_List_Should_Return_List_Of_FeatureFlags()
    {
        var splitManager = Substitute.For<ISplitManager>();
        splitManager.SplitNamesAsync().Returns(Task.FromResult(new List<string>
        {
            "some_flag",
            "some_other_flag",
            "some_missing_flag"
        }));

        var splitClient = Substitute.For<ISplitClient>();
        splitClient.GetTreatments("system_key", Arg.Is<List<string>>(names => names.SequenceEqual(new List<string>
        {
            "some_flag",
            "some_other_flag",
            "some_missing_flag"
        }))).Returns(new Dictionary<string, string>
        {
            { "some_flag", "on" },
            { "some_other_flag", "off" },
            { "some_missing_flag", "control" }
        });

        var splitFactory = Substitute.For<ISplitFactory>();
        splitFactory.Client().Returns(splitClient);
        splitFactory.Manager().Returns(splitManager);

        var splitOptions = new SplitIoOptions
        {
            SdkKey = "test_key",
            TreatmentKey = "system_key"
        };

        var splitOptionsWrapper = Options.Create(splitOptions);

        var factory = new Factory();

        var repository = new SplitIoFeatureFlagRepository(splitFactory, splitOptionsWrapper, factory);

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
                Id = "some_other_flag",
                Enabled = false
            }));
        });
    }

    [Test]
    public void SplitIoFeatureFlagRepository_Name_Should_Return_SplitIo()
    {
        var splitFactory = Substitute.For<ISplitFactory>();

        var splitOptions = new SplitIoOptions();

        var splitOptionsWrapper = Options.Create(splitOptions);

        var factory = new Factory();

        var repository = new SplitIoFeatureFlagRepository(splitFactory, splitOptionsWrapper, factory);

        Assert.That(repository.Name, Is.EqualTo("Split"));
    }
}