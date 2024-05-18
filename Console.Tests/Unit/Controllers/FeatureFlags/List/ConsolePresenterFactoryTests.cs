using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void ConsolePresenterFactory_Should_Be_A_IConsolePresenterFactory()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        var writer = Substitute.For<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizer, writer);

        Assert.That(factory, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void ConsolePresenterFactory_Create_Should_Return_ConsolePresenter()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        var writer = Substitute.For<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizer, writer);

        var presenter = factory.Create();

        Assert.That(presenter, Is.InstanceOf<IConsolePresenter>());
    }
}