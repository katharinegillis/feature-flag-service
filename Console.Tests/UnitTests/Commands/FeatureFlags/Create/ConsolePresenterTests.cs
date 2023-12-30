using Console.Commands.FeatureFlags.Create;
using Console.Common;
using Domain.Common;
using Microsoft.Extensions.Localization;
using Moq;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Create;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_Success_Message()
    {
        var localizerMock = new Mock<IStringLocalizer<ConsolePresenter>>();
        var createdString = new LocalizedString("Feature Flag \"{0}\" created.", "Feature Flag \"{0}\" created.");
        localizerMock.Setup(localizer => localizer["Feature Flag \"{0}\" created.", "some_flag"])
            .Returns(createdString);

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizerMock.Object, consoleWriterMock.Object);
        presenter.Ok("some_flag");

        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine(createdString));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_Display_Validation_Errors()
    {
        var localizerMock = new Mock<IStringLocalizer<ConsolePresenter>>();
        var validationString = new LocalizedString("{0}: {1}.", "{0}: {1}.");
        localizerMock.Setup(localizer => localizer["{0}: {1}.", It.IsAny<string>(), It.IsAny<LocalizedString>()])
            .Returns(validationString);
        var maxLengthString = new LocalizedString("Max length is 100", "Max length is 100");
        localizerMock.Setup(localizer => localizer["Max length is 100"])
            .Returns(maxLengthString);
        var requiredString = new LocalizedString("Required", "Required");
        localizerMock.Setup(localizer => localizer["Required"]).Returns(requiredString);

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizerMock.Object, consoleWriterMock.Object);
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

        localizerMock.Verify(localizer => localizer["Max length is 100"]);
        localizerMock.Verify(localizer => localizer["Required"]);
        localizerMock.Verify(localizer =>
            localizer["{0}: {1}.", "Id", maxLengthString]);
        localizerMock.Verify(
            localizer => localizer["{0}: {1}.", "Enabled", requiredString]);
        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine(validationString), Times.Exactly(2));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }

    [Test]
    public void ConsolePresenter_Error_Should_Display_Error_Message()
    {
        var localizerMock = new Mock<IStringLocalizer<ConsolePresenter>>();
        var errorString = new LocalizedString("Unknown error", "Unknown error");
        localizerMock.Setup(localizer => localizer["Unknown error"]).Returns(errorString);
        var fullErrorString = new LocalizedString("Error: {0}.", "Error: {0}.");
        localizerMock.Setup(localizer => localizer["Error: {0}.", errorString]).Returns(fullErrorString);

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizerMock.Object, consoleWriterMock.Object);
        presenter.Error(new Error
        {
            Message = "Unknown error"
        });

        localizerMock.Verify(localizer => localizer["Unknown error"]);
        localizerMock.Verify(localizer => localizer["Error: {0}.", errorString]);
        consoleWriterMock.Verify(consoleWriter => consoleWriter.WriteLine(fullErrorString));
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Error));
    }
}