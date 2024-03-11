using Application.Interactors.GetFeatureFlag;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Get;

[Category("Unit")]
public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlag()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock
            .Setup(s => s.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "True"))
            .Returns("Id: \"some_flag\", Enabled: \"True\"");
        localizerMock.Setup(s => s.Translate("true")).Returns("True");

        var writerMock = new Mock<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ConsolePresenter(request, localizerMock.Object, writerMock.Object);

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
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock.Setup(s => s.Translate("Feature Flag \"{0}\" doesn\'t exist.", "some_flag"))
            .Returns("Feature Flag \"some_flag\" doesn\'t exist.");

        var writerMock = new Mock<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ConsolePresenter(request, localizerMock.Object, writerMock.Object);

        presenter.NotFound();

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writerMock.Verify(w => w.WriteLine("Feature Flag \"some_flag\" doesn\'t exist."));
    }
}