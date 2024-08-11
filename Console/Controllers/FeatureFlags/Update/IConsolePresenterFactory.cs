using Application.Interactors.FeatureFlag.Update;

namespace Console.Controllers.FeatureFlags.Update;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}