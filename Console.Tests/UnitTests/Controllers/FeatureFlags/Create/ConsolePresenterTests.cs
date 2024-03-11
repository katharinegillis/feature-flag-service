using Application.Interactors.CreateFeatureFlag;
using Console.Controllers.FeatureFlags.Create;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Create;

[Category("Unit")]
public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_Success_Message()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock.Setup(s => s.Translate("Feature Flag \"{0}\" created.", "some_flag"))
            .Returns("Feature Flag \"some_flag\" created.");

        var writerMock = new Mock<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new ConsolePresenter(request, localizerMock.Object, writerMock.Object);
        presenter.Ok();

        writerMock.Verify(w => w.WriteLine("Feature Flag \"some_flag\" created."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_Display_Validation_Errors()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock.Setup(s => s.Translate("Required")).Returns("Required");
        localizerMock.Setup(s => s.Translate("Max length is 100")).Returns("Max length is 100");
        localizerMock.Setup(s => s.Translate("{0}: {1}.", "Id", "Max length is 100"))
            .Returns("Id: Max length is 100.");
        localizerMock.Setup(s => s.Translate("{0}: {1}.", "Enabled", "Required"))
            .Returns("Enabled: Required.");

        var writerMock = new Mock<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new ConsolePresenter(request, localizerMock.Object, writerMock.Object);
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

        writerMock.Verify(w => w.WriteLine("Id: Max length is 100."));
        writerMock.Verify(w => w.WriteLine("Enabled: Required."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }

    [Test]
    public void ConsolePresenter_Error_Should_Display_Error_Message()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock.Setup(s => s.Translate("Unknown error")).Returns("Unknown error");
        localizerMock.Setup(s => s.Translate("Error: {0}.", "Unknown error"))
            .Returns("Error: Unknown error.");

        var writerMock = new Mock<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new ConsolePresenter(request, localizerMock.Object, writerMock.Object);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        writerMock.Verify(w => w.WriteLine("Error: Unknown error."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Error));
    }
}