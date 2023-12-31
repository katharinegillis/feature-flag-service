using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using Domain.FeatureFlags;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlag()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock
            .Setup(s => s.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "True"))
            .Returns("Id: \"some_flag\", Enabled: \"True\"");
        localizationServiceMock.Setup(s => s.Translate("true")).Returns("True");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, writerMock.Object);

        presenter.Ok(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writerMock.Verify(w => w.WriteLine("Id: \"some_flag\", Enabled: \"True\""));
    }

    [Test]
    public void ConsolePresenter_NotFound_Should_Display_Not_Found()
    {
        var localizationServiceMock = new Mock<ILocalizationService<ConsolePresenter>>();
        localizationServiceMock.Setup(s => s.Translate("Feature Flag doesn\'t exist."))
            .Returns("Feature Flag doesn\'t exist.");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizationServiceMock.Object, writerMock.Object);

        presenter.NotFound();

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writerMock.Verify(w => w.WriteLine("Feature Flag doesn\'t exist."));
    }
}