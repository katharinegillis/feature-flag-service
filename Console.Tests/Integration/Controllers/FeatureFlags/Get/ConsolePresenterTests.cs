using Application.UseCases.FeatureFlag.Get;
using Console.Common;
using Console.Controllers.FeatureFlags.Get;
using Console.Localization;
using Domain.FeatureFlags;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Integration.Controllers.FeatureFlags.Get;

[Parallelizable]
[Category("Integration")]
public sealed class ConsolePresenterTests : AbstractControllerTest
{
    [Test]
    public void ConsolePresenter__Is_An_IConsolePresenter()
    {
        // Arrange
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        // Act
        var subject = new ConsolePresenter(request, localizer);

        // Assert
        Assert.That(subject, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter__Ok__Provides_Successful_ExitCode_And_Lines()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Id: \"some_flag\", Enabled: \"true\""
        };

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.Ok(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }

    [Test]
    public void ConsolePresenter__NotFound__Provides_Successful_ExitCode_And_Not_Found_Lines()
    {
        // Arrange
        var expectedLines = new List<string>
        {
            "Feature Flag \"some_flag\" doesn\'t exist."
        };

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        // Act
        var subject = new ConsolePresenter(request, SharedResourceLocalizationService);
        subject.NotFound();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(subject.ActionResult.ExitCode, Is.EqualTo((int)ExitCode.Success));
            Assert.That(subject.ActionResult.Lines, Is.EqualTo(expectedLines));
        });
    }
}