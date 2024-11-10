using Application.Interactors.FeatureFlag.IsEnabled;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Enabled;

public sealed class GetController(
    IActionResultPresenterFactory factory,
    IInputPort interactor,
    ILogger<GetController> logger)
    : ControllerBase
{
    [HttpGet("{id}/enabled")]
    public async Task<IActionResult> Execute(string id)
    {
        var request = new RequestModel
        {
            Id = id
        };

        var presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        if (presenter.IsError)
        {
            logger.LogError("{message}", presenter.Message);
        }

        return presenter.ActionResult;
    }
}