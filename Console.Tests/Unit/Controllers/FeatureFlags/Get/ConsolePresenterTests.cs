using Application.Interactors.FeatureFlag.Get;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Ok_Should_Display_FeatureFlag()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", "some_flag", "True")
            .Returns("Id: \"some_flag\", Enabled: \"True\"");
        localizer.Translate("true").Returns("True");

        var writer = Substitute.For<IConsoleWriter>();

        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ConsolePresenter(request, localizer, writer);

        presenter.Ok(new Model
        {
            Id = "some_flag",
            Enabled = true
        });

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writer.Received().WriteLine("Id: \"some_flag\", Enabled: \"True\"");
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
            Id = "some_flag"
        };

        var presenter = new ConsolePresenter(request, localizer, writer);

        presenter.NotFound();

        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));

        writer.Received().WriteLine("Feature Flag \"some_flag\" doesn\'t exist.");
    }
}