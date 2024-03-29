using Application.Interactors.IsFeatureFlagEnabled;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Controllers.Enabled;

[Category("Unit")]
public sealed class GetControllerTests
{
    [Test]
    public async Task Execute_Returns_True_If_Flag_Enabled()
    {
        var logger = Mock.Of<ILogger<GetController>>();

        var interactorMock = new Mock<IInputPort>();
        interactorMock
            .Setup(interactor => interactor.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));

        var presenterMock = new Mock<IActionResultPresenter>();
        presenterMock.Setup(p => p.ActionResult).Returns(new OkObjectResult(true));

        var factoryMock = new Mock<IActionResultPresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var controller = new GetController(factoryMock.Object, interactorMock.Object, logger);

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
        var logger = Mock.Of<ILogger<GetController>>();

        var interactorMock = new Mock<IInputPort>();
        interactorMock
            .Setup(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));

        var presenterMock = new Mock<IActionResultPresenter>();
        presenterMock.Setup(p => p.ActionResult).Returns(new OkObjectResult(false));

        var factoryMock = new Mock<IActionResultPresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var controller = new GetController(factoryMock.Object, interactorMock.Object, logger);

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
        var logger = Mock.Of<ILogger<GetController>>();

        var interactorMock = new Mock<IInputPort>();
        interactorMock.Setup(i => i.Execute(It.IsAny<RequestModel>(), It.IsAny<IOutputPort>()));

        var presenterMock = new Mock<IActionResultPresenter>();
        presenterMock.Setup(p => p.ActionResult).Returns(new NotFoundResult());

        var factoryMock = new Mock<IActionResultPresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var controller = new GetController(factoryMock.Object, interactorMock.Object, logger);

        var result = await controller.Execute("some_flag");

        Assert.That(result, Is.InstanceOf<NotFoundResult>());
    }

    [Test]
    public async Task Execute_Returns_InternalServerError_If_No_Action_Returned()
    {
        var loggerMock = new Mock<ILogger<GetController>>();

        var interactor = Mock.Of<IInputPort>();

        var presenterMock = new Mock<IActionResultPresenter>();
        presenterMock.Setup(p => p.ActionResult).Returns(new StatusCodeResult(500));
        presenterMock.Setup(p => p.IsError).Returns(true);
        presenterMock.Setup(p => p.Message).Returns("Error message");

        var factoryMock = new Mock<IActionResultPresenterFactory>();
        factoryMock.Setup(f => f.Create(It.IsAny<RequestModel>())).Returns(presenterMock.Object);

        var controller = new GetController(factoryMock.Object, interactor, loggerMock.Object);

        var result = await controller.Execute("some_flag");
        Assert.Multiple(() =>
        {
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            Assert.That((result as StatusCodeResult)?.StatusCode, Is.EqualTo(500));
        });

        loggerMock.Verify(l => l.Log(
                It.Is<LogLevel>(ll => ll == LogLevel.Error),
                It.Is<EventId>(e => e.Id == 0),
                It.Is<It.IsAnyType>((@object, type) =>
                    @object.ToString() == "Error message" && type.Name == "FormattedLogValues"),
                It.IsAny<Exception>(),
                It.IsAny<Func<It.IsAnyType, Exception, string>>()!),
            Times.Once);
    }
}