using Application.Interactors.GetFeatureFlag;
using Console.Controllers.FeatureFlags.Get;
using Console.Common;
using Moq;
using Controller = Console.Controllers.FeatureFlags.Get.Controller;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Get;

public class ControllerTests
{
    [Test]
    public void GetCommand_Should_Be_IRunnableWithOptions()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var command = new Controller(presenter, interactor);

        Assert.That(command, Is.InstanceOf<IRunnableWithOptions>());
    }

    [Test]
    public async Task GetCommand_Should_Return_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var interactorMock = new Mock<IInputPort>();

        var command = new Controller(presenterMock.Object, interactorMock.Object);

        var options = Mock.Of<IOptions>();

        command.SetOptions(options);

        var result = await command.Run();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}