using Application.Interactors.CreateFeatureFlag;
using Console.Commands.FeatureFlags.Create;
using Console.Common;
using Moq;

namespace Console.Tests.UnitTests.Commands.FeatureFlags.Create;

public class CommandTests
{
    [Test]
    public void CreateCommand_Should_Be_IRunnableWithOptions()
    {
        var presenter = Mock.Of<IConsolePresenter>();
        var interactor = Mock.Of<IInputPort>();

        var command = new Command(presenter, interactor);

        Assert.That(command, Is.InstanceOf<IRunnableWithOptions>());
    }

    [Test]
    public async Task CreateCommand_Creates_Flag()
    {
        var presenterMock = new Mock<IConsolePresenter>();
        presenterMock.Setup(presenter => presenter.ExitCode).Returns((int)ExitCode.Success);

        var createFeatureFlagInteractor = new Mock<IInputPort>();

        var command = new Command(presenterMock.Object, createFeatureFlagInteractor.Object);

        var optionsMock = new Mock<IOptions>();
        optionsMock.Setup(options => options.Id).Returns("some_flag");
        optionsMock.Setup(options => options.Enabled).Returns(true);

        command.SetOptions(optionsMock.Object);

        var result = await command.Run();

        Assert.That(result, Is.EqualTo((int)ExitCode.Success));

        createFeatureFlagInteractor.Verify(interactor =>
            interactor.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));
    }
}