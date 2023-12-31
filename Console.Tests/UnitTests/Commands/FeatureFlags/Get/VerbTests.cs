using CommandLine;
using Console.Commands.FeatureFlags.Get;
using Console.Common;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Get;

public class VerbTests
{
    [Test]
    public void GetVerb_Should_Be_A_IHasCommandType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasCommandType>());
    }

    [Test]
    public void GetVerb_Should_Associated_A_GetCommand()
    {
        var verb = new Verb();

        Assert.That(verb.CommandType, Is.EqualTo(typeof(Command)));
    }

    [Test]
    public void GetVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
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