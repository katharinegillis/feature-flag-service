using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.List;

public class ConsolePresenterFactoryTests
{
    [Test]
    public void ConsolePresenterFactory_Should_Be_A_IConsolePresenterFactory()
    {
        var localizerMock = new Mock<ILocalizationService<ConsolePresenter>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        Assert.That(factory, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void ConsolePresenterFactory_Create_Should_Return_ConsolePresenter()
    {
        var localizerMock = new Mock<ILocalizationService<ConsolePresenter>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        var presenter = factory.Create();

        Assert.That(presenter, Is.InstanceOf<IConsolePresenter>());
    }
}