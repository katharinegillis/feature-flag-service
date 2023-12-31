using Application.Interactors.GetFeatureFlag;
using Console.Commands.FeatureFlags.Get;
using Console.Common;
using Moq;
using Command = Console.Commands.FeatureFlags.Get.Command;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Get;

public class CommandTests
{
    [Test]
    public void GetCommand_Should_Be_IRunnableWithOptions()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var command = new Command(presenter, interactor);

        Assert.That(command, Is.InstanceOf<IRunnableWithOptions>());
    }

    [Test]
    public async Task GetCommand_Should_Return_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(presenter => presenter.ExitCode).Returns((int)ExitCode.Success);

        var interactorMock = new Mock<IInputPort>();

        var command = new Command(presenterMock.Object, interactorMock.Object);

        var options = Mock.Of<IOptions>();

        command.SetOptions(options);

        var result = await command.Run();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        interactorMock.Verify(interactor => interactor.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}