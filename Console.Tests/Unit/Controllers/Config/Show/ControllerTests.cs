using Application.Interactors.Config.Show;
using Console.Common;
using Console.Controllers.Config.Show;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.Config.Show;

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
    public async Task ShowController_Should_Succeed()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));
    }
}