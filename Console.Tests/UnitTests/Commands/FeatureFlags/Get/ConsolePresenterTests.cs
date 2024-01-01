using Console.Commands.FeatureFlags.Get;
using Console.Common;
using Domain.FeatureFlags;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Get;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlag()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock
            .Setup(service => service.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "true"))
            .Returns("Id: \"some_flag\", Enabled: \"true\"");

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, consoleWriterMock.Object);

        presenter.Ok(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        consoleWriterMock.Verify(writer => writer.WriteLine("Id: \"some_flag\", Enabled: \"true\""));
    }

    [Test]
    public void ConsolePresenter_NotFound_Should_Display_Not_Found()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(service => service.Translate("Feature Flag doesn\'t exist."))
            .Returns("Feature Flag doesn\'t exist.");

        var consoleWriterMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, consoleWriterMock.Object);

        presenter.NotFound();

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        consoleWriterMock.Verify(writer => writer.WriteLine("Feature Flag doesn\'t exist."));
    }
}