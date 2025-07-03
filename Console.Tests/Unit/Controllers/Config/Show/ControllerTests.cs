using Console.Common;
using Domain.Common;
using NSubstitute;
using ConfigShow = Application.UseCases.Config.Show;
using ConsoleConfigShow = Console.Controllers.Config.Show;

namespace Console.Tests.Unit.Controllers.Config.Show;

[Parallelizable]
[Category("Unit")]
public sealed class ControllerTests
{
    [Test]
    public void ConfigShowController__Is_An_IExecutable()
    {
        // Arrange
        var factory = Substitute.For<ConsoleConfigShow.IConsolePresenterFactory>();
        var interactor = Substitute.For<ConfigShow.IUseCase>();

        // Act
        var subject = new ConsoleConfigShow.Controller(factory, interactor);

        // Assert
        Assert.That(subject, Is.InstanceOf<IExecutable>());
    }

    [Test]
    public void ConfigShowController__Is_An_IOptions()
    {
        // Arrange
        var factory = Substitute.For<ConsoleConfigShow.IConsolePresenterFactory>();
        var interactor = Substitute.For<ConfigShow.IUseCase>();

        // Act
        var controller = new ConsoleConfigShow.Controller(factory, interactor);

        // Assert
        Assert.That(controller, Is.InstanceOf<IHasOptions>());
    }

    [Test]
    public async Task ConfigShowController__Execute__Should_Succeed_With_Datasource()
    {
        // Arrange
        var request = new ConfigShow.RequestModel
        {
            Name = ConfigShow.RequestModel.NameOptions.Datasource
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleConfigShow.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleConfigShow.IConsolePresenterFactory>();
        factory.Create(Arg.Any<ConfigShow.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<ConfigShow.IUseCase>();

        var options = Substitute.For<ConsoleConfigShow.IOptions>();
        options.Name.Returns("datasource");

        // Act
        var controller = new ConsoleConfigShow.Controller(factory, interactor);
        controller.SetOptions(options);
        var result = await controller.Execute();

        // Assert
        Assert.Multiple(() =>
        {
            interactor.Received().Execute(Arg.Is<ConfigShow.RequestModel>(x => x.Equals(request)), presenter);
            Assert.That(result, Is.SameAs(actionResult));
        });
    }

    [Test]
    public async Task ConfigShowController__Execute__Should_Succeed_With_Uppercase_Datasource()
    {
        // Arrange
        var request = new ConfigShow.RequestModel
        {
            Name = ConfigShow.RequestModel.NameOptions.Datasource
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleConfigShow.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleConfigShow.IConsolePresenterFactory>();
        factory.Create(Arg.Any<ConfigShow.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<ConfigShow.IUseCase>();

        var options = Substitute.For<ConsoleConfigShow.IOptions>();
        options.Name.Returns("DATASOURCE");

        // Act
        var controller = new ConsoleConfigShow.Controller(factory, interactor);
        controller.SetOptions(options);
        var result = await controller.Execute();

        // Assert
        Assert.Multiple(() =>
        {
            interactor.Received().Execute(Arg.Is<ConfigShow.RequestModel>(x => x.Equals(request)), presenter);
            Assert.That(result, Is.SameAs(actionResult));
        });
    }

    [Test]
    public async Task ConfigShowController__Execute__Calls_BadRequest_With_Unknown()
    {
        // Arrange
        var errors = new List<ValidationError>
        {
            new()
            {
                Message = "Must be one of: datasource",
                Field = "First argument"
            }
        };

        var actionResult = Substitute.For<IConsoleActionResult>();

        var presenter = Substitute.For<ConsoleConfigShow.IConsolePresenter>();
        presenter.ActionResult.Returns(actionResult);

        var factory = Substitute.For<ConsoleConfigShow.IConsolePresenterFactory>();
        factory.Create(Arg.Any<ConfigShow.RequestModel>()).Returns(presenter);

        var interactor = Substitute.For<ConfigShow.IUseCase>();

        var options = Substitute.For<ConsoleConfigShow.IOptions>();
        options.Name.Returns("unknown");

        // Act
        var controller = new ConsoleConfigShow.Controller(factory, interactor);
        controller.SetOptions(options);
        var result = await controller.Execute();

        // Assert
        Assert.Multiple(() =>
        {
            interactor.DidNotReceive().Execute(Arg.Any<ConfigShow.RequestModel>(), Arg.Any<ConfigShow.IPresenter>());
            presenter.Received().BadRequest(Arg.Is<List<ValidationError>>(x => errors.SequenceEqual(x)));
            Assert.That(result, Is.SameAs(actionResult));
        });
    }
}