using Application.UseCases.FeatureFlag.IsEnabled;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers.Enabled;

[ApiVersion(1)]
[ApiController]
[Route("api/v{v:apiVersion}/featureflags/{id}/enabled")]
public sealed class GetController(
    IActionResultPresenterFactory factory,
    IUseCase interactor)
    : ControllerBase
{
    [MapToApiVersion(1)]
    [HttpGet]
    public async Task<IActionResult> Execute(string id)
    {
        var request = new RequestModel
        {
            Id = id
        };

        var presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        return presenter.ActionResult;
    }
}