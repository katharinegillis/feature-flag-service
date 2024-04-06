using Application.Interactors.UpdateFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Update;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void UpdateController_Should_Be_Executable()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void UpdateController_Should_Have_Options()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task UpdateController_Updates_Flag()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        var options = Substitute.For<IOptions>();
        options.Id.Returns("some_flag");
        options.Enabled.Returns(false);

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());
    }
}