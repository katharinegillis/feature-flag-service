using Application.Interactors.GetFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Get;
using Console.Localization;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void ConsolePresenterFactory_Should_Be_A_IConsolePresenterFactory()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        Assert.That(factory, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void ConsolePresenterFactory_Should_Create_ConsolePresenter_With_Request()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        var presenter = factory.Create(request);

        Assert.That(presenter.Request, Is.EqualTo(request));
    }
}