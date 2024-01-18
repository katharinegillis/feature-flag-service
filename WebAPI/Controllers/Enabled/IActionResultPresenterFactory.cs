using Application.Interactors.IsFeatureFlagEnabled;

namespace WebAPI.Controllers.Enabled;

public interface IActionResultPresenterFactory
{
    public IActionResultPresenter Create(RequestModel request);
}