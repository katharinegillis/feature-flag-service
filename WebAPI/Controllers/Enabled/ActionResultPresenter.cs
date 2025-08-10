using Application.UseCases.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Common;

namespace WebAPI.Controllers.Enabled;

public sealed class ActionResultPresenter(RequestModel request) : IActionResultPresenter
{
    public IActionResult ActionResult { get; private set; } =
        new StatusCodeResult(StatusCodes.Status500InternalServerError);

    public void Ok(bool enabled)
    {
        ActionResult = new OkObjectResult(new ApiResponse<bool?>
        {
            Successful = true,
            Data = enabled,
        });
    }

    public void NotFound()
    {
        ActionResult = new OkObjectResult(new ApiResponse<bool?>
        {
            Successful = false,
            Errors = ["Not found"]
        });
    }

    public RequestModel Request => request;
}