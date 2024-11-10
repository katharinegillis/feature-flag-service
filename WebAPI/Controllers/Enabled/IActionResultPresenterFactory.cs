using Application.Interactors.FeatureFlag.IsEnabled;

namespace WebAPI.Controllers.Enabled;

public interface IActionResultPresenterFactory
{
    public IActionResultPresenter Create(RequestModel request);
}