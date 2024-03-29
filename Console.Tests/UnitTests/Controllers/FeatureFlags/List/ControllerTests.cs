using Application.Interactors.ListFeatureFlags;
using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Moq;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.List;

[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void ListController_Should_Be_Executable()
    {
        var factory = Mock.Of<IConsolePresenterFactory>();

        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public async Task ListController_Should_Return_Successful()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var factoryMock = new Mock<IConsolePresenterFactory>();
        factoryMock.Setup(f => f.Create()).Returns(presenterMock.Object);

        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(factoryMock.Object, interactorMock.Object);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<IOutputPort>()));
    }
}