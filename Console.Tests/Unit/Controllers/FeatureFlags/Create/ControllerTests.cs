using Console.Common;
using NSubstitute;
using FeatureFlagCreate = Application.UseCases.FeatureFlag.Create;
using ConsoleFeatureFlagCreate = Console.Controllers.FeatureFlags.Create;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Create;

[Parallelizable]
[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void FeatureFlagCreateController__Is_An_IExecutable()
    {
        // Arrange
        var interactor = Substitute.For<FeatureFlagCreate.IUseCase>();
        var factory = Substitute.For<ConsoleFeatureFlagCreate.IConsolePresenterFactory>();

        // Act
        var subject = new ConsoleFeatureFlagCreate.Controller(factory, interactor);

        // Arrange
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void FeatureFlagCreateController__Is_An_IOptions()
    {
        //Arrange
        var factory = Substitute.For<ConsoleFeatureFlagCreate.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagCreate.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagCreate.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task FeatureFlagCreateController__Execute__Creates_Flag()
    {
        // Arrange
        const string flagId = "new_flag";
        const bool enabled = true;

        var request = new FeatureFlagCreate.RequestModel
        {
            Id = flagId,
            Enabled = enabled
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleFeatureFlagCreate.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleFeatureFlagCreate.IConsolePresenterFactory>();
        factory.Create(Arg.Any<FeatureFlagCreate.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<FeatureFlagCreate.IUseCase>();

        var options = Substitute.For<ConsoleFeatureFlagCreate.IOptions>();
        options.Id.Returns(flagId);
        options.Enabled.Returns(enabled);

        // Act
        var subject = new ConsoleFeatureFlagCreate.Controller(factory, interactor);
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