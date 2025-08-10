using Application.UseCases.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Unit.Controllers.Enabled;

[Parallelizable]
[Category("Unit")]
public sealed class ActionResultPresenterTests
{
    [Test]
    public void ActionResultPresenter_ActionResult_Should_Default_To_InternalServerError()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        Assert.Multiple(() =>
        {
            Assert.That(presenter.ActionResult, Is.InstanceOf<StatusCodeResult>());
            Assert.That((presenter.ActionResult as StatusCodeResult)?.StatusCode, Is.EqualTo(500));
        });
    }

    [Test]
    public void ActionResultPresenter_Ok_Should_Create_Ok_Response_With_Given_Data()
    {
        // Arrange
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        // Act
        presenter.Ok(true);

        // Assert
        Assert.That(presenter.ActionResult, Is.InstanceOf<OkObjectResult>());
        var okResult = presenter.ActionResult as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<ApiResponse<bool?>>());
        var response = okResult.Value as ApiResponse<bool?>;
        
        Assert.Multiple(() =>
        {
            Assert.That(response!.Successful, Is.True);
            Assert.That(response.Data, Is.True);
            Assert.That(response.Errors, Is.Null);
        });
    }

    [Test]
    public void ActionResultPresenter_NotFound_Should_Create_Ok_Response_With_Not_Found_Error()
    {
        // Arrange
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        // Act
        presenter.NotFound();

        // Assert
        Assert.That(presenter.ActionResult, Is.InstanceOf<OkObjectResult>());
        var okResult = presenter.ActionResult as OkObjectResult;
        Assert.That(okResult!.Value, Is.InstanceOf<ApiResponse<bool?>>());
        var response = okResult.Value as ApiResponse<bool?>;
        
        Assert.Multiple(() =>
        {
            Assert.That(response!.Successful, Is.False);
            Assert.That(response.Data, Is.Null);
            Assert.That(response.Errors, Is.EqualTo(new List<string> { "Not found" }));
        });
    }
}