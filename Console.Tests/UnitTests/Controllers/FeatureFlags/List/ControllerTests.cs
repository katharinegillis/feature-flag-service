using Application.Interactors.ListFeatureFlags;
using Console.Common;
using Console.Controllers.FeatureFlags.List;
using Moq;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.List;

public sealed class ControllerTests
{
    [Test]
    public void ListController_Should_Be_IRunnableWithOptions()
    {
        var presenter = Mock.Of<IConsolePresenter>();

        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(presenter, interactor);

        Assert.That(controller, Is.InstanceOf<IRunnableWithOptions>());
    }

    [Test]
    public async Task ListController_Should_Return_Successful()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var interactorMock = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, interactorMock.Object);

        var options = new object();

        controller.SetOptions(options);

        var result = await controller.Run();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(i => i.Execute(It.IsAny<IOutputPort>()));
    }
}