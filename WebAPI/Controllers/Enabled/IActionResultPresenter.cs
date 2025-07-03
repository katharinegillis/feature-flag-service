using Application.UseCases.FeatureFlag.IsEnabled;
using WebAPI.Common;

namespace WebAPI.Controllers.Enabled;

public interface IActionResultPresenter : IPresenter, IHasActionResult, IHasIsError
{
    public RequestModel Request { get; }
}