using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Console.Localization;
using Domain.FeatureFlags;
using Moq;
using Utilities.LocalizationService;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.List;

[Category("Unit")]
public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlags()
    {
        var localizerMock = new Mock<ILocalizationService<SharedResource>>();
        localizerMock.Setup(s => s.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "True"))
            .Returns("Id: \"some_flag\", Enabled: \"True\"");
        localizerMock.Setup(s => s.Translate("true")).Returns("True");
        localizerMock.Setup(s => s.Translate("Id: \"{0}\", Enabled: \"{1}\"", "another_flag", "False"))
            .Returns("Id: \"another_flag\", Enabled: \"False\"");
        localizerMock.Setup(s => s.Translate("false")).Returns("False");

        var writerMock = new Mock<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizerMock.Object, writerMock.Object);

        presenter.Ok(new List<IModel>
        {
            new Model
            {
                Id = "some_flag",
                Enabled = true
            },
            new Model
            {
                Id = "another_flag",
                Enabled = false
            }
        });

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writerMock.Verify(w => w.WriteLine("Id: \"some_flag\", Enabled: \"True\""));
        writerMock.Verify(w => w.WriteLine("Id: \"another_flag\", Enabled: \"False\""));
    }
}