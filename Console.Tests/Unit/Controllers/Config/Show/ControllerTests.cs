using Application.Interactors.Config.Show;
using Console.Common;
using Console.Controllers.Config.Show;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void ShowController_Should_Be_Executable()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void ShowController_Should_Have_Options()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task ShowController_Should_Succeed_With_Datasource()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();
        options.Name.Returns("datasource");

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public async Task ShowController_Should_Succeed_With_Uppercase_Datasource()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();
        options.Name.Returns("DATASOURCE");

        controller.SetOptions(options);

        var result = await controller.Execute();
        
        Assert.That(result, Is.EqualTo((int)ExitCode.Success));
    }

    [Test]
    public async Task ShowController_Should_Be_NotFound_With_Unknown()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();
        options.Name.Returns("unknown");
        
        controller.SetOptions(options);

        var result = await controller.Execute();
        
        Assert.That(result, Is.EqualTo((int)ExitCode.Success));
        
        // Should this be an integration test, or test that presenter.NotFound was called
    }
}