using Application.Interactors.FeatureFlag.Get;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using NSubstitute;
using Controller = Console.Controllers.FeatureFlags.Get.Controller;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

public sealed class ControllerTests
{
    [Test]
    public void GetController_Should_Be_Executable()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void GetController_Should_Have_Options()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task GetController_Should_Successful()
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

        await interactor.Received().Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());
    }
}