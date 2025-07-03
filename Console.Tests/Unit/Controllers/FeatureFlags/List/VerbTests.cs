using CommandLine;
using Console.Common;
using ConsoleFeatureFlagList = Console.Controllers.FeatureFlags.List;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void FeatureFlagListVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleFeatureFlagList.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void FeatureFlagListVerb__Should_Associate_A_ListController()
    {
        // Act
        var subject = new ConsoleFeatureFlagList.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleFeatureFlagList.Controller)));
    }

    [Test]
    public void FeatureFlagListVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagList.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void FeatureFlagListVerb__Has_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleFeatureFlagList.Verb), typeof(ReadOnlyVerbAttribute)));
    }
}