using Console.Localization;
using NSubstitute;
using Utilities.LocalizationService;
using FeatureFlagCreate = Application.UseCases.FeatureFlag.Create;
using ConsoleFeatureFlagCreate = Console.Controllers.FeatureFlags.Create;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Create;

[Parallelizable]
[Category("Unit")]
public sealed class ConsolePresenterFactoryTests
{
    [Test]
    public void FeatureFlagCreateConsolePresenterFactory__Is_A_IConsolePresenterFactory()
    {
        // Arrange
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagCreate.ConsolePresenterFactory(localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagCreate.IConsolePresenterFactory>());
    }

    [Test]
    public void FeatureFlagCreateConsolePresenterFactory__Create__Provides_A_New_Presenter_With_Request()
    {
        // Arrange
        const string flagId = "some_flag";
        const bool enabled = true;

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsoleFeatureFlagCreate.ConsolePresenterFactory(localizer);
        var result1 = subject.Create(request);
        var result2 = subject.Create(request);

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1.Request, Is.EqualTo(request));
            Assert.That(result1, Is.Not.SameAs(result2));
        });
    }
}