using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Console.Localization;
using Domain.FeatureFlags;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlags()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "True")
            .Returns("Id: \"some_flag\", Enabled: \"True\"");
        localizer.Translate("true").Returns("True");
        localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", "another_flag", "False")
            .Returns("Id: \"another_flag\", Enabled: \"False\"");
        localizer.Translate("false").Returns("False");

        var writer = Substitute.For<IConsoleWriter>();

        var presenter = new ConsolePresenter(localizer, writer);

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

        writer.Received().WriteLine("Id: \"some_flag\", Enabled: \"True\"");
        writer.Received().WriteLine("Id: \"another_flag\", Enabled: \"False\"");
    }
}