using Application.UseCases.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using WebAPI.Common;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Unit.Controllers.Enabled;

[Parallelizable]
[Category("Unit")]
public sealed class GetControllerTests
{
    [Test]
    public async Task Execute_Returns_True_If_Flag_Enabled()
    {
        // Arrange
        var interactor = Substitute.For<IUseCase>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IPresenter>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new OkObjectResult(new ApiResponse<bool?>
        {
            Successful = true,
            Data = true
        }));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor);

        // Act
        var result = await controller.Execute("some_flag");
        
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<ApiResponse<bool?>>());
        var response = okResult.Value as ApiResponse<bool?>;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(response!.Successful, Is.True);
            Assert.That(response.Data, Is.True);
        }
    }

    [Test]
    public async Task Execute_Returns_False_If_Flag_Disabled()
    {
        // Arrange
        var interactor = Substitute.For<IUseCase>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IPresenter>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new OkObjectResult(new ApiResponse<bool?>
        {
            Successful = true,
            Data = false
        }));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor);

        // Act
        var result = await controller.Execute("some_flag");
        
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<ApiResponse<bool?>>());
        var response = okResult.Value as ApiResponse<bool?>;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(response!.Successful, Is.True);
            Assert.That(response.Data, Is.False);
        }
    }

    [Test]
    public async Task Execute_Returns_NotFound_If_Flag_Not_Found()
    {
        // Arrange
        var interactor = Substitute.For<IUseCase>();
        await interactor.Execute(Arg.Any<RequestModel>(), Arg.Any<IPresenter>());

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new OkObjectResult(new ApiResponse<bool?>
        {
            Successful = false,
            Errors = ["Not found"]
        }));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor);

        // Act
        var result = await controller.Execute("some_flag");

        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<ApiResponse<bool?>>());
        var response = okResult.Value as ApiResponse<bool?>;

        using (Assert.EnterMultipleScope())
        {
            Assert.That(response!.Successful, Is.False);
            Assert.That(response.Errors, Is.EqualTo(new List<string>
            {
                "Not found"
            }));
        }
    }

    [Test]
    public async Task Execute_Returns_InternalServerError_If_No_Action_Returned()
    {
        // Arrange
        var interactor = Substitute.For<IUseCase>();

        var presenter = Substitute.For<IActionResultPresenter>();
        presenter.ActionResult.Returns(new StatusCodeResult(500));

        var factory = Substitute.For<IActionResultPresenterFactory>();
        factory.Create(Arg.Any<RequestModel>()).Returns(presenter);

        var controller = new GetController(factory, interactor);

        // Act
        var result = await controller.Execute("some_flag");

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(result, Is.InstanceOf<StatusCodeResult>());
            Assert.That((result as StatusCodeResult)?.StatusCode, Is.EqualTo(500));
        }
    }
}