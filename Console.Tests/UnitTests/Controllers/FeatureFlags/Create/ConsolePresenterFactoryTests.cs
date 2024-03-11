using Application.Interactors.CreateFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Create;
using Console.Localization;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Create;

[Category("Unit")]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void ConsolePresenterFactory_Is_A_IConsolePresenterFactory()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        Assert.That(factory, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void ConsolePresenterFactory_Should_Create_Presenter_With_Request()
    {
        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = true
        };

        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        var writerMock = new Mock<IConsoleWriter>();

        var factory = new ConsolePresenterFactory(localizerMock.Object, writerMock.Object);

        var presenter = factory.Create(request);

        Assert.That(presenter.Request, Is.EqualTo(request));
    }
}