using Console.Common;
using Domain.Common;
using FeatureFlagDelete = Application.UseCases.FeatureFlag.Delete;
using ConsoleFeatureFlagDelete = Console.Controllers.FeatureFlags.Delete;

namespace Console.Tests.Integration.Controllers.FeatureFlags.Delete;

[Parallelizable]
public sealed class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void FeatureFlagDeleteConsolePresenter__Is_An_IConsolePresenter()
    {
        // Arrange
        const string flagId = "some_flag";

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenter(request, SharedResourceLocalizationService);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagDelete.IConsolePresenter>());
    }

    [Test]
    public void FeatureFlagDeleteConsolePresenter__Ok__Displays_Success_Message()
    {
        // Arrange
        const string flagId = "some_flag";
        var expectedLines = new List<string>
        {
            $"Feature Flag \"{flagId}\" deleted."
        };

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Ok();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }

    [Test]
    public void FeatureFlagConsolePresenter__Error__Displays_Error_Message()
    {
        // Arrange
        const string flagId = "some_flag";

        var expectedLines = new List<string>
        {
            "Error: Unknown error."
        };

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Error(new Error
        {
            Message = "Unknown error"
        });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Error));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }

    [Test]
    public void FeatureFlagDeleteConsolePresenter__NotFound__Displays_Not_Found()
    {
        // Arrange
        const string flagId = "some_flag";

        var expectedLines = new List<string>
        {
            $"Feature Flag \"{flagId}\" doesn't exist."
        };

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        // Act
        var subject = new ConsoleFeatureFlagDelete.ConsolePresenter(request, SharedResourceLocalizationService);
        subject.NotFound();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }
}