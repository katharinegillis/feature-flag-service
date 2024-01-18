using Application.Interactors.UpdateFeatureFlag;

namespace Console.Controllers.FeatureFlags.Update;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}