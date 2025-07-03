using CommandLine;
using Console.Common;
using ConsoleFeatureFlagGet = Console.Controllers.FeatureFlags.Get;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void FeatureFlagGetVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleFeatureFlagGet.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void FeatureFlagGetVerb__Should_Associate_A_GetController()
    {
        // Act
        var subject = new ConsoleFeatureFlagGet.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleFeatureFlagGet.Controller)));
    }

    [Test]
    public void FeatureFlagGetVerb__Is_An_IOptions()
    {
        // Act
        var subject = new ConsoleFeatureFlagGet.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagGet.IOptions>());
    }

    [Test]
    public void FeatureFlagGetVerb__Id__Initializes_Id()
    {
        // Act
        var subject = new ConsoleFeatureFlagGet.Verb
        {
            Id = "some_flag"
        };

        // Assert
        Assert.That(subject.Id, Is.EqualTo("some_flag"));
    }

    [Test]
    public void FeatureFlagGetVerb__Id__Sets_Id()
    {
        // Act
        var subject = new ConsoleFeatureFlagGet.Verb
        {
            Id = "some_flag"
        };
        subject.Id = "another_flag";

        // Assert
        Assert.That(subject.Id, Is.EqualTo("another_flag"));
    }

    [Test]
    public void FeatureFlagGetVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagGet.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void FeatureFlagGetVerb__Has_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagGet.Verb), typeof(ReadOnlyVerbAttribute)));
    }

    [Test]
    public void FeatureFlagGetVerb__Id__Property_Has_Option_Attribute()
    {
        // Act
        var t = typeof(ConsoleFeatureFlagGet.Verb);
        var property = t.GetProperty("Id");

        // Assert
        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}