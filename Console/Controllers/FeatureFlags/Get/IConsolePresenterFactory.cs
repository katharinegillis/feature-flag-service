using Application.Interactors.FeatureFlag.Get;

namespace Console.Controllers.FeatureFlags.Get;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}