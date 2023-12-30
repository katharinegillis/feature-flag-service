using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Enabled;

public sealed class ActionResultPresenter : IPresenter
{
    public IActionResult ActionResult { get; private set; } =
        new StatusCodeResult(StatusCodes.Status500InternalServerError);

    public bool IsError { get; private set; } = true;
    public string? Message { get; private set; } = "No action result set";

    public void Ok(bool enabled)
    {
        ActionResult = new OkObjectResult(enabled);
        IsError = false;
        Message = null;
    }

    public void NotFound()
    {
        ActionResult = new NotFoundResult();
        IsError = false;
        Message = null;
    }
}