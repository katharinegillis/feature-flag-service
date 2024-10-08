using Application.Interactors.Config.Show;
using Console.Common;
using Console.Controllers.Config.Show;
using Domain.Common;

namespace Console.Tests.Unit.Controllers.Config.Show;

public class ConsolePresenterTests
{
    [Test]
    public void ConsolePresenter_Should_Be_An_IConsolePresenter()
    {
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };
        
        var presenter = new ConsolePresenter(request);
        
        Assert.That(presenter, Is.InstanceOf<IConsolePresenter>());
    }

    [Test]
    public void ConsolePresenter_Ok_Should_ExitCode_Successful()
    {
        var request = new RequestModel
        {
            Name = RequestModel.NameOptions.Datasource
        };

        var presenter = new ConsolePresenter(request);
        
        presenter.Ok("Some datasource");
        
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public void ConsolePresenter_BadRequest_Should_ExitCode_OptionsError()
    {
        var presenter = new ConsolePresenter(null);
        
        presenter.BadRequest(new List<ValidationError>());
        
        Assert.That(presenter.ExitCode, Is.EqualTo((int)ExitCode.OptionsError));
    }
}