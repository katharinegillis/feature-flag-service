using Application.Interactors.FeatureFlag.Get;
using Console.Common;
using Console.Controllers.FeatureFlags.Get;
using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

[Category("Unit")]
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
    public void ConsolePresenterFactory_Should_Create_ConsolePresenter_With_Request()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        var writer = Substitute.For<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizer, writer);

        var presenter = factory.Create(request);

        Assert.That(presenter.Request, Is.EqualTo(request));
    }
}