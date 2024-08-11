using Application.Interactors.FeatureFlag.Delete;
using Console.Common;
using Console.Controllers.FeatureFlags.Delete;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Delete;

public sealed class ControllerTests
{
    [Test]
    public void DeleteController_Should_Be_Executable()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void DeleteController_Should_Have_Options()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task DeleteController_Deletes_Flag()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();
        options.Id.Returns("some_flag");

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        await interactor.Received().Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());
    }
}