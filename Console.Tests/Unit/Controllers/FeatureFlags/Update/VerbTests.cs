using CommandLine;
using Console.Common;
using Console.Controllers.FeatureFlags.Update;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

public sealed class VerbTests
{
    [Test]
    public void UpdateVerb_Should_Be_A_IHasControllerType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void UpdateVerb_Should_Associate_An_UpdateController()
    {
        var verb = new Verb();

        Assert.That(verb.ControllerType, Is.EqualTo(typeof(Controller)));
    }

    [Test]
    public void UpdateVerb_Should_Be_A_IOptions()
    {
        var verb = new Verb();
        Assert.That(verb, Is.InstanceOf<IOptions>());
    }

    [Test]
    public void UpdateVerb_Should_Have_An_Id()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Id = "some_flag";

        Assert.That(verb.Id, Is.EqualTo("some_flag"));
    }

    [Test]
    public void UpdateVerb_Should_Have_Enabled_Flag()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Enabled = true;

        Assert.That(verb.Enabled, Is.True);
    }

    [Test]
    public void UpdateVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }
    
    [Test]
    public void UpdateVerb_Should_Not_Be_A_ReadOnlyVerb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(ReadOnlyVerbAttribute)), Is.False);
    }

    [Test]
    public void UpdateVerb_Id_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Id");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }

    [Test]
    public void UpdateVerb_Enabled_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Enabled");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}