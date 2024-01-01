using Console.Commands.FeatureFlags.Create;
using Console.Common;
using Domain.Common;
using Microsoft.Extensions.Localization;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Create;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_Success_Message()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(service => service.Translate("Feature Flag \"{0}\" created.", "some_flag"))
            .Returns("Feature Flag \"some_flag\" created.");

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, consoleWriterMock.Object);
        presenter.Ok("some_flag");

        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine("Feature Flag \"some_flag\" created."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_Display_Validation_Errors()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(service => service.Translate("Required")).Returns("Required");
        localizationServiceMock.Setup(service => service.Translate("Max length is 100")).Returns("Max length is 100");
        localizationServiceMock.Setup(service => service.Translate("{0}: {1}.", "Id", "Max length is 100"))
            .Returns("Id: Max length is 100.");
        localizationServiceMock.Setup(service => service.Translate("{0}: {1}.", "Enabled", "Required"))
            .Returns("Enabled: Required.");

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, consoleWriterMock.Object);
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

        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine("Id: Max length is 100."));
        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine("Enabled: Required."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }

    [Test]
    public void ConsolePresenter_Error_Should_Display_Error_Message()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(service => service.Translate("Unknown error")).Returns("Unknown error");
        localizationServiceMock.Setup(service => service.Translate("Error: {0}.", "Unknown error"))
            .Returns("Error: Unknown error.");

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, consoleWriterMock.Object);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine("Error: Unknown error."));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Error));
    }
}