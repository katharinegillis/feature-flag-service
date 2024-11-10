using Application.Interactors.FeatureFlag.Delete;

namespace Console.Controllers.FeatureFlags.Delete;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}