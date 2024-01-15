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
        var interactorMock = new Mock<IInputPort>();
        var factoryMock = new Mock<IConsolePresenterFactory>();

        var controller = new Controller(factoryMock.Object, interactorMock.Object);

        Assert.That(controller, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void CreateController_Should_Have_Options()
    {
        var factory = Mock.Of<IConsolePresenterFactory>();
        var interactor = Mock.Of<IInputPort>();

        var controller = new Controller(factory, interactor);

        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task CreateController_Creates_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(p => p.ExitCode).Returns((int)ExitCode.Success);

        var factoryMock = new Mock<IConsolePresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var createFeatureFlagInteractor = new Mock<IInputPort>();

        var controller = new Controller(factoryMock.Object, createFeatureFlagInteractor.Object);

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