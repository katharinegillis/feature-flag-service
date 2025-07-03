using Application.UseCases.FeatureFlag.Get;
using Console.Controllers.FeatureFlags.Get;
using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

[Parallelizable]
[Category("Unit")]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void FeatureFlagGetConsolePresenterFactory__Is_An_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<IConsolePresenterFactory>());
    }

    [Test]
    public void FeatureFlagGetConsolePresenterFactory__Create__Creates_ConsolePresenter_With_Request()
    {
        // Arrange
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsolePresenterFactory(localizer);
        var result = subject.Create(request);

        // Assert
        Assert.That(result.Request, Is.EqualTo(request));
    }
}