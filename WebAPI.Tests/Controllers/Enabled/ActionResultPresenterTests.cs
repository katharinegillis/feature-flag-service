using Microsoft.AspNetCore.Mvc;
using WebAPI.Controllers.Enabled;

namespace WebAPI.Tests.Controllers.Enabled;

public sealed class ActionResultPresenterTests
{
    [Test]
    public void ActionResultPresenter_ActionResult_Should_Default_To_InternalServerError()
    {
        var presenter = new ActionResultPresenter();

        Assert.Multiple(() =>
        {
            Assert.That(presenter.ActionResult, Is.InstanceOf<StatusCodeResult>());
            Assert.That((presenter.ActionResult as StatusCodeResult)?.StatusCode, Is.EqualTo(500));
        });
    }

    [Test]
    public void ActionResultPresenter_IsError_Should_Default_To_True()
    {
        var presenter = new ActionResultPresenter();

        Assert.That(presenter.IsError, Is.True);
    }

    [Test]
    public void ActionResultPresenter_Message_Should_Default_To_NoActionSet()
    {
        var presenter = new ActionResultPresenter();

        Assert.That(presenter.Message, Is.EqualTo("No action result set"));
    }

    [Test]
    public void ActionResultPresenter_Ok_Should_Create_Ok_Response_With_Given_Data()
    {
        var presenter = new ActionResultPresenter();

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
        var presenter = new ActionResultPresenter();

        presenter.NotFound();

        Assert.Multiple(() =>
        {
            Assert.That(presenter.ActionResult, Is.InstanceOf<NotFoundResult>());
            Assert.That(presenter.IsError, Is.False);
            Assert.That(presenter.Message, Is.Null);
        });
    }
}