using Console.Commands.FeatureFlags.Create;
using Console.Common;
using Domain.Common;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Create;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_Success_Message()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(s => s.Translate("Feature Flag \"{0}\" created.", "some_flag"))
            .Returns("Feature Flag \"some_flag\" created.");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, writerMock.Object);
        presenter.Ok("some_flag");

        writerMock.Verify(w => w.WriteLine("Feature Flag \"some_flag\" created."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_Display_Validation_Errors()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(s => s.Translate("Required")).Returns("Required");
        localizationServiceMock.Setup(s => s.Translate("Max length is 100")).Returns("Max length is 100");
        localizationServiceMock.Setup(s => s.Translate("{0}: {1}.", "Id", "Max length is 100"))
            .Returns("Id: Max length is 100.");
        localizationServiceMock.Setup(s => s.Translate("{0}: {1}.", "Enabled", "Required"))
            .Returns("Enabled: Required.");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, writerMock.Object);
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
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(s => s.Translate("Unknown error")).Returns("Unknown error");
        localizationServiceMock.Setup(s => s.Translate("Error: {0}.", "Unknown error"))
            .Returns("Error: Unknown error.");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, writerMock.Object);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        writerMock.Verify(w => w.WriteLine("Error: Unknown error."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Error));
    }
}