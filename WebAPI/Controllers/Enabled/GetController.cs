using Application.Interactors.IsFeatureFlagEnabled;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Enabled;

public sealed class GetController(IPresenter presenter, IInputPort interactor, ILogger<GetController> logger)
    : ControllerBase
{
    [HttpGet("{id}/enabled")]
    public async Task<IActionResult> Execute(string id)
    {
        await interactor.Execute(new RequestModel
        {
            Id = id
        }, presenter);

        if (presenter.IsError)
        {
            logger.LogError("{message}", presenter.Message);
        }

        return presenter.ActionResult;
    }
}