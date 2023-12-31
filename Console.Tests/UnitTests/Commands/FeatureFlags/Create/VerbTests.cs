using CommandLine;
using Console.Commands.FeatureFlags.Create;
using Console.Common;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Create;

public class VerbTests
{
    [Test]
    public void CreateVerb_Should_Be_A_IHasCommandType()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IHasCommandType>());
    }

    [Test]
    public void CreateVerb_Should_Associate_A_CreateCommand()
    {
        var verb = new Verb();

        Assert.That(verb.CommandType, Is.EqualTo(typeof(Command)));
    }

    [Test]
    public void CreateVerb_Should_Be_A_IOptions()
    {
        var verb = new Verb();

        Assert.That(verb, Is.InstanceOf<IOptions>());
    }

    [Test]
    public void CreateVerb_Should_Have_An_Id()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Id = "new_flag";

        Assert.That(verb.Id, Is.EqualTo("new_flag"));
    }

    [Test]
    public void CreateVerb_Should_Have_Enabled_Flag()
    {
        // ReSharper disable once UseObjectOrCollectionInitializer
        var verb = new Verb();
        verb.Enabled = true;

        Assert.That(verb.Enabled);
    }

    [Test]
    public void CreateVerb_Should_Be_A_Verb()
    {
        Assert.That(Attribute.IsDefined(typeof(Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void CreateVerb_Id_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Id");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }

    [Test]
    public void CreateVerb_Enabled_Property_Should_Be_An_Option()
    {
        var t = typeof(Verb);
        var property = t.GetProperty("Enabled");

        Assert.That(property != null && Attribute.IsDefined(property, typeof(OptionAttribute)));
    }
}