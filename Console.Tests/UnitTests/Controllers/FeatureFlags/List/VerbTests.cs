using CommandLine;
using Console.Common;
using Console.Controllers.FeatureFlags.List;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.List;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void ListVerb_Should_Be_A_IHasControllerType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void ListVerb_Should_Be_Associated_With_ListController()
    {
        var verb = new Verb();

        Assert.That(verb.ControllerType, Is.EqualTo(typeof(Controller)));
    }

    [Test]
    public void ListVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }
}