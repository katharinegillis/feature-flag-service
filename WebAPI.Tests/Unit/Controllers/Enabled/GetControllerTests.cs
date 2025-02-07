using Application.Interactors.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Unit.Controllers.Enabled;

[Category("Unit")]
public sealed class GetControllerTests
{
    [Test]
    public async Task Execute_Returns_True_If_Flag_Enabled()
    {
        var logger = Substitute.For<ILogger<GetController>>();

        var interactor = Substitute.For<IInputPort>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new OkObjectResult(true));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor, logger);

        var result = await controller.Execute("some_flag");
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That((result as OkObjectResult)?.Value, Is.True);
        });
    }

    [Test]
    public async Task Execute_Returns_False_If_Flag_Disabled()
    {
        var logger = Substitute.For<ILogger<GetController>>();

        var interactor = Substitute.For<IInputPort>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new OkObjectResult(false));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor, logger);

        var result = await controller.Execute("some_flag");
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<OkObjectResult>());
            Assert.That((result as OkObjectResult)?.Value, Is.False);
        });
    }

    [Test]
    public async Task Execute_Returns_NotFound_If_Flag_Not_Found()
    {
        var logger = Substitute.For<ILogger<GetController>>();

        var interactor = Substitute.For<IInputPort>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IOutputPort>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new NotFoundResult());

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor, logger);

        var result = await controller.Execute("some_flag");

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Execute_Returns_InternalServerError_If_No_Action_Returned()
    {
        var logger = Substitute.For<ILogger<GetController>>();

        var interactor = Substitute.For<IInputPort>();

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new StatusCodeResult(500));
        presenter.IsError.Returns(true);
        presenter.Message.Returns("Error message");

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor, logger);

        var result = await controller.Execute("some_flag");
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            Assert.That((result as StatusCodeResult)?.StatusCode, Is.EqualTo(500));
        });

        Assert.That(logger.ReceivedCalls()
                .Select(call => call.GetArguments())
                .Count(callArguments => ((LogLevel)callArguments[0]!).Equals(LogLevel.Error) &&
                                        ((IReadOnlyList<KeyValuePair<string, object>>)callArguments[2]!)[
                                            ((IReadOnlyList<KeyValuePair<string, object>>)callArguments[2]!).Count - 1]
                                        .Value
                                        .ToString()!.Equals("Error message")),
            Is.EqualTo(0));
    }
}