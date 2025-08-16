using Console.Common;
using NSubstitute;
using FeatureFlagList = Application.UseCases.FeatureFlag.List;
using ConsoleFeatureFlagList = Console.Controllers.FeatureFlags.List;

namespace Console.Tests.Unit.Controllers.FeatureFlags.List;

[Parallelizable]
[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void FeatureFlagListController__Is_An_Executable()
    {
        // Arrange
        var factory = Substitute.For<ConsoleFeatureFlagList.IConsolePresenterFactory>();
        var interactor = Substitute.For<FeatureFlagList.IUseCase>();

        // Act
        var subject = new ConsoleFeatureFlagList.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public async Task FeatureFlagListController__Execute__Lists_Flags()
    {
        // Arrange
        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleFeatureFlagList.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleFeatureFlagList.IConsolePresenterFactory>();
        factory.Create().Returns(presenter);

        var interactor = Substitute.For<FeatureFlagList.IUseCase>();

        // Act
        var controller = new ConsoleFeatureFlagList.Controller(factory, interactor);
        var result = await controller.Execute();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            await interactor.Received().Execute(presenter);
            Assert.That(result, Is.SameAs(actionResult));
        }
    }
}