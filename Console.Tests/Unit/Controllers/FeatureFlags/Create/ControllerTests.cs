using Application.Interactors.FeatureFlag.Create;
using Console.Controllers.FeatureFlags.Create;
using Console.Common;
using NSubstitute;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Create;

public sealed class ControllerTests
{
    [Test]
    public void CreateController_Should_Be_Executable()
    {
        var interactor = Substitute.For<IInputPort>();
        var factory = Substitute.For<IConsolePresenterFactory>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void CreateController_Should_Have_Options()
    {
        var factory = Substitute.For<IConsolePresenterFactory>();
        var interactor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task CreateController_Creates_Flag()
    {
        var presenter = Substitute.For<IConsolePresenter>();
        presenter.ExitCode.Returns((int)ExitCode.Success);

        var factory = Substitute.For<IConsolePresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var createFeatureFlagInteractor = Substitute.For<IInputPort>();

        var controller = new Controller(factory, createFeatureFlagInteractor);

        var options = Substitute.For<IOptions>();
        options.Id.Returns("some_flag");
        options.Enabled.Returns(true);

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        await createFeatureFlagInteractor.Received().Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());
    }
}