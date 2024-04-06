using CommandLine;
using Console.Common;
using Console.Controllers.FeatureFlags.Delete;
using Verb = Console.Controllers.FeatureFlags.Delete.Verb;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Delete;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void DeleteVerb_Should_Be_An_IHasControllerType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void DeleteVerb_Should_Associate_A_DeleteController()
    {
        var verb = new Verb();

        Assert.That(verb.ControllerType, Is.EqualTo(typeof(Controller)));
    }

    [Test]
    public void DeleteVerb_Should_Be_A_IOptions()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IOptions>());
    }

    [Test]
    public void DeleteVerb_Should_Have_An_Id()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Id = "some_flag";

        Assert.That(verb.Id, Is.EqualTo("some_flag"));
    }

    [Test]
    public void DeleteVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void DeleteVerb_Id_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Id");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}