using CommandLine;
using Console.Common;
using ConsoleFeatureFlagDelete = Console.Controllers.FeatureFlags.Delete;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Delete;

[Parallelizable]
[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void FeatureFlagDeleteVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleFeatureFlagDelete.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void FeatureFlagDeleteVerb__Should_Associate_A_DeleteController()
    {
        // Act
        var subject = new ConsoleFeatureFlagDelete.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleFeatureFlagDelete.Controller)));
    }

    [Test]
    public void FeatureFlagDeleteVerb__Is_An_IOptions()
    {
        // Act
        var subject = new ConsoleFeatureFlagDelete.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagDelete.IOptions>());
    }

    [Test]
    public void FeatureFlagDeleteVerb__Id__Initializes_Id()
    {
        // Act
        const string flagId = "some_flag";
        var subject = new ConsoleFeatureFlagDelete.Verb
        {
            Id = flagId
        };

        // Assert
        Assert.That(subject.Id, Is.EqualTo(flagId));
    }

    [Test]
    public void FeatureFlagDeleteVerb__Id__Sets_Id()
    {
        // Act
        const string initFlagId = "some_flag";
        const string setFlagId = "another_flag";
        var subject = new ConsoleFeatureFlagDelete.Verb
        {
            Id = initFlagId
        };
        subject.Id = setFlagId;

        // Assert
        Assert.That(subject.Id, Is.EqualTo(setFlagId));
    }

    [Test]
    public void FeatureFlagDeleteVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagDelete.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void FeatureFlagDeleteVerb__Does_Not_Have_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagDelete.Verb), typeof(ReadOnlyVerbAttribute)),
            Is.False);
    }

    [Test]
    public void FeatureFlagDeleteVerb__Id__Property_Has_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagDelete.Verb);
        var property = t.GetProperty("Id");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}