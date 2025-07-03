using Console.Common;
using NSubstitute;
using FeatureFlagUpdate = Application.UseCases.FeatureFlag.Update;
using ConsoleFeatureFlagUpdate = Console.Controllers.FeatureFlags.Update;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Update;

[Parallelizable]
[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void FeatureFlagUpdateController__Is_An_Executable()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagUpdate.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagUpdate.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagUpdate.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void FeatureFlagUpdateController__Is_An_IOptions()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagUpdate.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagUpdate.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagUpdate.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task FeatureFlagUpdateController__Execute__Updates_Flag()
    {
        // Arrange
        var request = new FeatureFlagUpdate.RequestModel
        {
            Id = "some_flag",
            Enabled = false
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleFeatureFlagUpdate.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleFeatureFlagUpdate.IConsolePresenterFactory>();
        factory.Create(Arg.Any<FeatureFlagUpdate.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<FeatureFlagUpdate.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagUpdate.Controller(factory, interactor);

        var options = Substitute.For<ConsoleFeatureFlagUpdate.IOptions>();
        options.Id.Returns("some_flag");
        options.Enabled.Returns(false);

        subject.SetOptions(options);

        var result = await subject.Execute();

        // Assert
        Assert.Multiple(() =>
        {
            interactor.Received().Execute(request, presenter);
            Assert.That(result, Is.SameAs(actionResult));
        });
    }
}