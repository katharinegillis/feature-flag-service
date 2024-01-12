using Application.Interactors.CreateFeatureFlag;
using Console.Controllers.FeatureFlags.Create;
using Console.Common;
using Moq;

namespace Console.Tests.UnitTests.Controllers.FeatureFlags.Create;

public sealed class ControllerTests
{
    [Test]
    public void CreateController_Should_Be_Executable()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(presenter, interactor);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void CreateController_Should_Have_Options()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(presenter, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task CreateController_Creates_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var createFeatureFlagInteractor = new Mock<IInputPort>();

        var controller = new Controller(presenterMock.Object, createFeatureFlagInteractor.Object);

        var optionsMock = new Mock<IOptions>();
        optionsMock.Setup(o => o.Id).Returns("some_flag");
        optionsMock.Setup(o => o.Enabled).Returns(true);

        controller.SetOptions(optionsMock.Object);

        var result = await controller.Execute();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        createFeatureFlagInteractor.Verify(i =>
            i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}