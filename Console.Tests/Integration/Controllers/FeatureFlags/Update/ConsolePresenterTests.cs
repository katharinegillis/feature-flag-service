using Application.UseCases.FeatureFlag.Update;
using Console.Common;
using Console.Controllers.FeatureFlags.Update;
using Domain.Common;

namespace Console.Tests.Integration.Controllers.FeatureFlags.Update;

[Category("Unit")]
public sealed class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void ConsolePresenter__Is_An_IConsolePresenter()
    {
        // Arrange
        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = true
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);

        // Assert
        Assert.That(subject, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter__Ok__Provides_Successful_ExitCode_And_Lines()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Feature Flag \"some_flag\" updated."
        };

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Ok();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }

    [Test]
    public void ConsolePresenter__BadRequest__Displays_Validation_Errors()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Id: Max length is 100.",
            "Enabled: Required."
        };

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.BadRequest(new List<ValidationError>
        {
            new()
            {
                Field = "Id",
                Message = "Max length is 100"
            },
            new()
            {
                Field = "Enabled",
                Message = "Required"
            }
        });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }

    [Test]
    public void ConsolePresenter__Error__Displays_Error_Message()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Error: Unknown error."
        };

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
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
    public void ConsolePresenter__NotFound__Displays_Not_Found()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Feature Flag \"some_flag\" doesn\'t exist."
        };

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.NotFound();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }
}