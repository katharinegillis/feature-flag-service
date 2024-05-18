using Application.Interactors.UpdateFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Update;
using Console.Localization;
using Domain.Common;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_Success_Message()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Feature Flag \"{0}\" updated.", "some_flag")
            .Returns("Feature Flag \"some_flag\" updated.");

        var writer = Substitute.For<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = new ConsolePresenter(request, localizer, writer);
        presenter.Ok();

        writer.Received().WriteLine("Feature Flag \"some_flag\" updated.");
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_Display_Validation_Errors()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Required").Returns("Required");
        localizer.Translate("Max length is 100").Returns("Max length is 100");
        localizer.Translate("{0}: {1}.", "Id", "Max length is 100").Returns("Id: Max length is 100.");
        localizer.Translate("{0}: {1}.", "Enabled", "Required").Returns("Enabled: Required.");

        var writer = Substitute.For<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = new ConsolePresenter(request, localizer, writer);
        presenter.BadRequest(new List<ValidationError>
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

        writer.Received().WriteLine("Id: Max length is 100.");
        writer.Received().WriteLine("Enabled: Required.");
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }

    [Test]
    public void ConsolePresenter_Error_Should_Display_Error_Message()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Unknown error").Returns("Unknown error");
        localizer.Translate("Error: {0}.", "Unknown error").Returns("Error: Unknown error.");

        var writer = Substitute.For<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = new ConsolePresenter(request, localizer, writer);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        writer.Received().WriteLine("Error: Unknown error.");
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Error));
    }

    [Test]
    public void ConsolePresenter_NotFound_Should_Display_Not_Found()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", "some_flag")
            .Returns("Feature Flag \"some_flag\" doesn\'t exist.");

        var writer = Substitute.For<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var presenter = new ConsolePresenter(request, localizer, writer);

        presenter.NotFound();

        writer.Received().WriteLine("Feature Flag \"some_flag\" doesn\'t exist.");
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }
}