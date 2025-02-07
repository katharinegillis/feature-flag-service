using CommandLine;
using Console.Common;
using Console.Controllers.Config.Show;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void ShowVerb_Should_Be_A_IHasControllerType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void ShowVerb_Should_Associate_A_ShowController()
    {
        var verb = new Verb();

        Assert.That(verb.ControllerType, Is.EqualTo(typeof(Controller)));
    }

    [Test]
    public void ShowVerb_Should_Be_A_IOptions()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IOptions>());
    }

    [Test]
    public void ShowVerb_Should_Have_Name()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Name = "datasource";

        Assert.That(verb.Name, Is.EqualTo("datasource"));
    }

    [Test]
    public void ShowVerb_Name_Property_Should_Be_A_Value()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Name");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(ValueAttribute)));
    }

    [Test]
    public void ShowVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void ShowVerb_Should_Be_A_ReadonlyVerb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(ReadOnlyVerbAttribute)));
    }
}