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
}