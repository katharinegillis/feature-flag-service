using CommandLine;
using Console.Common;
using ConsoleConfigShow = Console.Controllers.Config.Show;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Parallelizable]
[Category("Unit")]
public sealed class VerbTests
{
    [Test]
    public void ConfigShowVerb__Is_An_IHasControllerType()
    {
        // Act
        var subject = new ConsoleConfigShow.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasControllerType>());
    }

    [Test]
    public void ConfigShowVerb__Should_Associate_A_ShowController()
    {
        // Act
        var subject = new ConsoleConfigShow.Verb();

        // Assert
        Assert.That(subject.ControllerType, Is.EqualTo(typeof(ConsoleConfigShow.Controller)));
    }

    [Test]
    public void ConfigShowVerb__Is_An_IOptions()
    {
        // Act
        var subject = new ConsoleConfigShow.Verb();

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleConfigShow.IOptions>());
    }

    [Test]
    public void ConfigShowVerb__Name__Initializes_Name()
    {
        // Act
        const string configName = "datasource";
        var subject = new ConsoleConfigShow.Verb
        {
            Name = configName
        };

        // Assert
        Assert.That(subject.Name, Is.EqualTo(configName));
    }

    [Test]
    public void ConfigShowVerb__Name__Sets_Name()
    {
        // Act
        const string initConfigName = "datasource";
        const string setConfigName = "name";
        var subject = new ConsoleConfigShow.Verb
        {
            Name = initConfigName
        };
        subject.Name = setConfigName;

        // Assert
        Assert.That(subject.Name, Is.EqualTo(setConfigName));
    }

    [Test]
    public void ConfigShowVerb__Name__Property_Has_Value_Attribute()
    {
        // Act
        var t = typeof(ConsoleConfigShow.Verb);
        var subject = t.GetProperty("Name");

        // Assert
        Assert.That(subject != null && Attribute.IsDefined(subject, typeof(ValueAttribute)));
    }

    [Test]
    public void ConfigShowVerb__Has_A_Verb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleConfigShow.Verb), typeof(VerbAttribute)));
    }

    [Test]
    public void ConfigShowVerb__Has_A_ReadOnlyVerb_Attribute()
    {
        // Assert
        Assert.That(Attribute.IsDefined(typeof(ConsoleConfigShow.Verb), typeof(ReadOnlyVerbAttribute)));
    }
}