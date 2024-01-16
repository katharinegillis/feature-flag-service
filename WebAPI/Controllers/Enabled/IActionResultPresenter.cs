using Application.Interactors.IsFeatureFlagEnabled;
using WebAPI.Common;

namespace WebAPI.Controllers.Enabled;

public interface IActionResultPresenter : IOutputPort, IHasActionResult, IHasIsError
{
    public RequestModel Request { get; }
}