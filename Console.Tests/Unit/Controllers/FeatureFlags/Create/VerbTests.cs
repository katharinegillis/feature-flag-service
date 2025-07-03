using CommandLine;
using Console.Common;
using ConsoleFeatureFlagCreate = Console.Controllers.FeatureFlags.Create;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Create;

[Parallelizable]
[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void FeatureFlagCreateVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleFeatureFlagCreate.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void FeatureFlagCreateVerb__Should_Associate_A_CreateController()
    {
        // Act
        var subject = new ConsoleFeatureFlagCreate.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleFeatureFlagCreate.Controller)));
    }

    [Test]
    public void FeatureFlagCreateVerb__Is_An_IOptions()
    {
        // Act
        var subject = new ConsoleFeatureFlagCreate.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagCreate.IOptions>());
    }

    [Test]
    public void FeatureFlagCreateVerb__Id__Initializes_Id()
    {
        // Act
        const string flagId = "new_flag";
        var subject = new ConsoleFeatureFlagCreate.Verb
        {
            Id = flagId
        };

        // Assert
        Assert.That(subject.Id, Is.EqualTo(flagId));
    }

    [Test]
    public void FeatureFlagCreateVerb__Id__Sets_Id()
    {
        // Act
        const string initFlagId = "new_flag";
        const string setFlagId = "second_flag";
        var subject = new ConsoleFeatureFlagCreate.Verb
        {
            Id = initFlagId
        };
        subject.Id = setFlagId;

        // Assert
        Assert.That(subject.Id, Is.EqualTo(setFlagId));
    }

    [Test]
    public void FeatureFlagCreateVerb__Enabled__Initializes_Enabled_Flag()
    {
        // Act
        const bool enabled = true;
        var subject = new ConsoleFeatureFlagCreate.Verb
        {
            Enabled = enabled
        };

        // Assert
        Assert.That(subject.Enabled, Is.EqualTo(enabled));
    }

    [Test]
    public void FeatureFlagCreateVerb__Enabled__Sets_Enabled_Flag()
    {
        // Act
        const bool initEnabled = true;
        const bool setEnabled = false;
        var subject = new ConsoleFeatureFlagCreate.Verb
        {
            Enabled = initEnabled
        };
        subject.Enabled = setEnabled;

        // Assert
        Assert.That(subject.Enabled, Is.EqualTo(setEnabled));
    }

    [Test]
    public void FeatureFlagCreateVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagCreate.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void FeatureFlagCreateVerb__Does_Not_Have_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagCreate.Verb), typeof(ReadOnlyVerbAttribute)),
            Is.False);
    }

    [Test]
    public void FeatureFlagCreateVerb__Id__Property_Has_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagCreate.Verb);
        var property = t.GetProperty("Id");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }

    [Test]
    public void FeatureFlagCreateVerb__Enabled__Property_Has_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagCreate.Verb);
        var property = t.GetProperty("Enabled");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}