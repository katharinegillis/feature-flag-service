using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;
using FeatureFlagList = Console.Controllers.FeatureFlags.List;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

[Category("Unit")]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void FeatureFlagListConsolePresenterFactory__Is_An_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new FeatureFlagList.ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<FeatureFlagList.IConsolePresenterFactory>());
    }

    [Test]
    public void FeatureFlagListConsolePresenterFactory__Create__Creates_ConsolePresenter()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new FeatureFlagList.ConsolePresenterFactory(localizer);
        var result = subject.Create();

        // Assert
        Assert.That(result, Is.InstanceOf<FeatureFlagList.IConsolePresenter>());
    }
}