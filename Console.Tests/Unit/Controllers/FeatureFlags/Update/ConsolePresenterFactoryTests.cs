using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;
using FeatureFlagUpdate = Application.UseCases.FeatureFlag.Update;
using ConsoleFeatureFlagUpdate = Console.Controllers.FeatureFlags.Update;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

[Parallelizable]
[Category("Unit")]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void FeatureFlagUpdateConsolePresenterFactory__Is_An_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagUpdate.ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagUpdate.IConsolePresenterFactory>());
    }

    [Test]
    public void FeatureFlagUpdateConsolePresenterFactory__Create__Creates_ConsolePresenter_With_Request()
    {
        // Arrange
        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagUpdate.ConsolePresenterFactory(localizer);
        var result = subject.Create(request);

        // Assert
        Assert.That(result.Request, Is.EqualTo(request));
    }
}