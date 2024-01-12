using Application.Interactors.GetFeatureFlag;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using Moq;
using Controller = Console.Controllers.FeatureFlags.Get.Controller;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Get;

public sealed class ControllerTests
{
    [Test]
    public void GetController_Should_Be_Executable()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(presenter, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void GetController_Should_Have_Options()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(presenter, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task GetController_Should_Successful()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, interactorMock.Object);

        var options = Mock.Of<IOptions>();

        controller.SetOptions(options);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}