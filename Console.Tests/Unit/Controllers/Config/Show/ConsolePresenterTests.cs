using Application.Interactors.Config.Show;
using Console.Common;
using Console.Controllers.Config.Show;
using Console.Localization;
using Domain.Common;
using NSubstitute;
using Utilities.LocalizationService;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Category("Unit")]
public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Should_Be_An_IConsolePresenter()
    {
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };
        
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();

        var writer = Substitute.For<IConsoleWriter>();
        
        var presenter = new ConsolePresenter(request, localizer, writer);
        
        Assert.That(presenter, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter_Ok_Should_ExitCode_Successful()
    {
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };
        
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("Datasource \"{0}\"", "Some datasource").Returns("Datasource \"Some datasource\"");

        var writer = Substitute.For<IConsoleWriter>();

        var presenter = new ConsolePresenter(request, localizer, writer);
        
        presenter.Ok("Some datasource");
        
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_ExitCode_OptionsError()
    {
        var localizer = Substitute.For<ILocalizationService<SharedResource>>();
        localizer.Translate("First argument").Returns("First argument");
        localizer.Translate("Must be one of: datasource").Returns("Must be one of: datasource");
        localizer.Translate("{0}: {1}.", "First argument", "Must be one of: datasource").Returns("First argument: Must be one of: datasource.");

        var writer = Substitute.For<IConsoleWriter>();
        
        var presenter = new ConsolePresenter(null, localizer, writer);
        
        presenter.BadRequest(new List<ValidationError>());
        
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }
}