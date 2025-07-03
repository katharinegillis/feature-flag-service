using Console.Common;
using NSubstitute;
using FeatureFlagGet = Application.UseCases.FeatureFlag.Get;
using ConsoleFeatureFlagGet = Console.Controllers.FeatureFlags.Get;
using Controller = Console.Controllers.FeatureFlags.Get.Controller;

namespace Console.Tests.Unit.Controllers.FeatureFlags.Get;

[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void FeatureFlagGetController__Is_An_IExecutable()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagGet.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagGet.IUseCase>();

        // Act
        var subject = new Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void FeatureFlagGetController__Is_An_IOptions()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagGet.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagGet.IUseCase>();

        // Act
        var subject = new Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task FeatureFlagGetController__Execute__Should_Succeed()
    {
        // Arrange
        var request = new FeatureFlagGet.RequestModel
        {
            Id = "some_flag"
        };

        var actionResult = Substitute.For<IConsoleActionResult>();


        var presenter = Substitute.For<ConsoleFeatureFlagGet.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleFeatureFlagGet.IConsolePresenterFactory>();
        factory.Create(Arg.Any<FeatureFlagGet.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<FeatureFlagGet.IUseCase>();

        var options = Substitute.For<ConsoleFeatureFlagGet.IOptions>();
        options.Id.Returns("some_flag");

        // Act
        var controller = new Controller(factory, interactor);
        controller.SetOptions(options);
        var result = await controller.Execute();

        // Assert
        Assert.Multiple(() =>
        {
            interactor.Received().Execute(request, presenter);
            Assert.That(result, Is.SameAs(actionResult));
        });
    }
}