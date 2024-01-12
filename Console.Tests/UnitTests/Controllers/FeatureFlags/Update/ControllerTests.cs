using Application.Interactors.UpdateFeatureFlag;
using Console.Common;
using Console.Controllers.FeatureFlags.Update;
using Moq;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Update;

public sealed class ControllerTests
{
    [Test]
    public void UpdateController_Should_Be_Executable()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, interactorMock.Object);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void UpdateController_Should_Have_Options()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, interactorMock.Object);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task UpdateController_Updates_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, interactorMock.Object);

        var optionsMock = new Mock<IOptions>();
        optionsMock.Setup(o => o.Id).Returns("some_flag");
        optionsMock.Setup(o => o.Enabled).Returns(false);

        controller.SetOptions(optionsMock.Object);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}