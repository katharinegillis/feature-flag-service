using Application.Interactors.ListFeatureFlags;
using Console.Common;
using Console.Controllers.FeatureFlags.List;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

public sealed class ControllerTests
{
    [Test]
    public void ListController_Should_Be_Executable()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public async Task ListController_Should_Return_Successful()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create().Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        await interactor.Execute(Arg.Any<IOutputPort>());
    }
}