using CommandLine;
using Console.Common;
using ConsoleFeatureFlagUpdate = Console.Controllers.FeatureFlags.Update;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void FeatureFlagUpdateVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void FeatureFlagUpdateVerb__Should_Associate_An_UpdateController()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleFeatureFlagUpdate.Controller)));
    }

    [Test]
    public void FeatureFlagUpdateVerb__Is_An_IOptions()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagUpdate.IOptions>());
    }

    [Test]
    public void FeatureFlagUpdateVerb__Id__Initializes_Id()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb
        {
            Id = "some_flag"
        };

        // Assert
        Assert.That(subject.Id, Is.EqualTo("some_flag"));
    }

    [Test]
    public void FeatureFlagUpdateVerb__Id__Sets_Id()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb
        {
            Id = "some_flag"
        };
        subject.Id = "another_flag";

        // Assert
        Assert.That(subject.Id, Is.EqualTo("another_flag"));
    }

    [Test]
    public void FeatureFlagUpdateVerb__Enabled__Initializes_Enabled()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb
        {
            Enabled = true
        };

        // Assert
        Assert.That(subject.Enabled, Is.True);
    }

    [Test]
    public void FeatureFlagUpdateVerb__Enabled__Sets_Enabled()
    {
        // Act
        var subject = new ConsoleFeatureFlagUpdate.Verb
        {
            Enabled = true
        };
        subject.Enabled = false;

        // Assert
        Assert.That(subject.Enabled, Is.False);
    }

    [Test]
    public void FeatureFlagUpdateVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagUpdate.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void FeatureFlagUpdateVerb__Does_Not_Have_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagUpdate.Verb), typeof(ReadOnlyVerbAttribute)),
            Is.False);
    }

    [Test]
    public void FeatureFlagUpdateVerb__Id__Property_Has_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagUpdate.Verb);
        var property = t.GetProperty("Id");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }

    [Test]
    public void FeatureFlagUpdateVerb__Enabled__Property_Has_An_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagUpdate.Verb);
        var property = t.GetProperty("Enabled");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}