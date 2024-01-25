using Application.Interactors.DeleteFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Delete;
using Moq;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Delete;

public sealed class ControllerTests
{
    [Test]
    public void DeleteController_Should_Be_Executable()
    {
        var factoryMock = new Mock<IConsolePresenterFactory>();
        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(factoryMock.Object, interactorMock.Object);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void DeleteController_Should_Have_Options()
    {
        var factoryMock = new Mock<IConsolePresenterFactory>();
        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(factoryMock.Object, interactorMock.Object);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task DeleteController_Deletes_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var factoryMock = new Mock<IConsolePresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(factoryMock.Object, interactorMock.Object);

        var optionsMock = new Mock<IOptions>();
        optionsMock.Setup(o => o.Id).Returns("some_flag");

        controller.SetOptions(optionsMock.Object);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}