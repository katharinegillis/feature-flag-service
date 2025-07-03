using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;
using ConfigShow = Application.UseCases.Config.Show;
using ConsoleConfigShow = Console.Controllers.Config.Show;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Parallelizable]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void ConfigShowConsolePresenterFactory__Is_An_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleConfigShow.ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleConfigShow.IConsolePresenterFactory>());
    }

    [Test]
    public void ConfigShowConsolePresenterFactory__Create__Creates_ConsolePresenter_With_Request()
    {
        // Arrange
        var request = new ConfigShow.RequestModel();

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleConfigShow.ConsolePresenterFactory(localizer);
        var result = subject.Create(request);

        // Assert
        Assert.That(result.Request, Is.SameAs(request));
    }
}