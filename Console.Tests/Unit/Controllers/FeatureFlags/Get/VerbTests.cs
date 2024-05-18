using CommandLine;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

public sealed class VerbTests
{
    [Test]
    public void GetVerb_Should_Be_A_IHasControllerType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void GetVerb_Should_Be_Associated_With_GetController()
    {
        var verb = new Verb();

        Assert.That(verb.ControllerType, Is.EqualTo(typeof(Controller)));
    }

    [Test]
    public void GetVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }
    
    [Test]
    public void GetVerb_Should_Be_A_ReadOnlyVerb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(ReadOnlyVerbAttribute)));
    }

    [Test]
    public void GetVerb_Should_Be_IOptions()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IOptions>());
    }

    [Test]
    public void GetVerb_Should_Have_Id()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Id = "new_flag";

        Assert.That(verb.Id, Is.EqualTo("new_flag"));
    }

    [Test]
    public void GetVerb_Id_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Id");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}