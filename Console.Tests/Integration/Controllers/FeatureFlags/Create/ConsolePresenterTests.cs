using Console.Common;
using Domain.Common;
using FeatureFlagCreate = Application.UseCases.FeatureFlag.Create;
using ConsoleFeatureFlagCreate = Console.Controllers.FeatureFlags.Create;

namespace Console.Tests.Integration.Controllers.FeatureFlags.Create;

[Parallelizable]
[Category("Integration")]
public sealed class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void FeatureFlagCreateConsolePresenter__Is_An_IConsolePresenter()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        // Act
        var subject = new ConsoleFeatureFlagCreate.ConsolePresenter(request, SharedResourceLocalizationService);

        // Assert
        Assert.That(subject, Is.InstanceOf<ConsoleFeatureFlagCreate.IConsolePresenter>());
    }

    [Test]
    public void FeatureFlagCreateConsolePresenter__Ok__Displays_Success_Message()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var expectedLines = new List<string>
        {
            $"Feature Flag \"{flagId}\" created."
        };

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        // Act
        var subject = new ConsoleFeatureFlagCreate.ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Ok();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        }
    }

    [Test]
    public void FeatureFlagCreateConsolePresenter__BadRequest__Displays_Validation_Errors()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var expectedLines = new List<string>
        {
            "Id: Max length is 100.",
            "Enabled: Required."
        };

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        // Act
        var subject = new ConsoleFeatureFlagCreate.ConsolePresenter(request, SharedResourceLocalizationService);
        subject.BadRequest(new List<ValidationError>
        {
            new()
            {
                Field = "Id",
                Message = "Max length is 100."
            },
            new()
            {
                Field = "Enabled",
                Message = "Required."
            }
        });

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        }
    }

    [Test]
    public void FeatureFlagCreateConsolePresenter__Error__Displays_Error_Message()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var expectedLines = new List<string>
        {
            "Error: Unknown error."
        };

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        // Act
        var presenter = new ConsoleFeatureFlagCreate.ConsolePresenter(request, SharedResourceLocalizationService);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(presenter.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Error));
            Assert.That(presenter.ActionResult.Lines, Is.EqualTo(expectedLines));
        }
    }
}