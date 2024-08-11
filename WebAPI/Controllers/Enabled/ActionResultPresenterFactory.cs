using Application.Interactors.FeatureFlag.IsEnabled;

namespace WebAPI.Controllers.Enabled;

public sealed class ActionResultPresenterFactory : IActionResultPresenterFactory
{
    public IActionResultPresenter Create(RequestModel request)
    {
        return new ActionResultPresenter(request);
    }
}