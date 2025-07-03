using Console.Common;
using NSubstitute;
using FeatureFlagDelete = Application.UseCases.FeatureFlag.Delete;
using ConsoleFeatureFlagDelete = Console.Controllers.FeatureFlags.Delete;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Delete;

[Parallelizable]
public sealed class ControllerTests
{
    [Test]
    public void FeatureFlagDeleteController__Is_An_IExecutable()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagDelete.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagDelete.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagDelete.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void FeatureFlagDeleteController__Is_An_IOptions()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagDelete.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagDelete.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagDelete.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task FeatureFlagDeleteController__Execute__Deletes_Flag()
    {
        // Arrange
        const string flagId = "some_flag";

        var request = new FeatureFlagDelete.RequestModel
        {
            Id = flagId
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleFeatureFlagDelete.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleFeatureFlagDelete.IConsolePresenterFactory>();
        factory.Create(Arg.Any<FeatureFlagDelete.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<FeatureFlagDelete.IUseCase>();

        var options = Substitute.For<ConsoleFeatureFlagDelete.IOptions>();
        options.Id.Returns(flagId);

        // Act
        var subject = new ConsoleFeatureFlagDelete.Controller(factory, interactor);
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