using Application.Interactors.IsFeatureFlagEnabled;

namespace WebAPI.Controllers.Enabled;

public class ActionResultPresenterFactory : IActionResultPresenterFactory
{
    public IActionResultPresenter Create(RequestModel request)
    {
        return new ActionResultPresenter(request);
    }
}