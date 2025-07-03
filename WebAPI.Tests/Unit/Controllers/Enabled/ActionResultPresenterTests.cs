using Application.UseCases.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Unit.Controllers.Enabled;

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
    public void ActionResultPresenter_IsError_Should_Default_To_True()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        Assert.That(presenter.IsError, Is.True);
    }

    [Test]
    public void ActionResultPresenter_Message_Should_Default_To_NoActionSet()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        Assert.That(presenter.Message, Is.EqualTo("No action result set"));
    }

    [Test]
    public void ActionResultPresenter_Ok_Should_Create_Ok_Response_With_Given_Data()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        presenter.Ok(true);

        Assert.Multiple(() =>
        {
            Assert.That(presenter.ActionResult, Is.InstanceOf<OkObjectResult>());
            Assert.That((presenter.ActionResult as OkObjectResult)?.Value, Is.True);
            Assert.That(presenter.IsError, Is.False);
            Assert.That(presenter.Message, Is.Null);
        });
    }

    [Test]
    public void ActionResultPresenter_NotFound_Should_Create_NotFound_Response()
    {
        var request = new RequestModel
        {
            Id = "some_flag"
        };

        var presenter = new ActionResultPresenter(request);

        presenter.NotFound();

        Assert.Multiple(() =>
        {
            Assert.That(presenter.ActionResult, Is.InstanceOf<NotFoundResult>());
            Assert.That(presenter.IsError, Is.False);
            Assert.That(presenter.Message, Is.Null);
        });
    }
}