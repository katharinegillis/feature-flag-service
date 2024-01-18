using Application.Interactors.GetFeatureFlag;

namespace Console.Controllers.FeatureFlags.Get;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}