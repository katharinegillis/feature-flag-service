using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;
using FeatureFlagDelete = Application.UseCases.FeatureFlag.Delete;
using ConsoleFeatureFlagDelete = Console.Controllers.FeatureFlags.Delete;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Delete;

[Parallelizable]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void FeatureFlagDeleteConsolePresenterFactory__Is_An_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagDelete.IConsolePresenterFactory>());
    }

    [Test]
    public void FeatureFlagDeleteConsolePresenterFactory__Create__Creates_ConsolePresenter_With_Request()
    {
        // Arrange
        const string flagId = "some_flag";
        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenterFactory(localizer);
        var result = subject.Create(request);

        // Assert
        Assert.That(result.Request, Is.EqualTo(request));
    }
}