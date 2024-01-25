using Application.Interactors.DeleteFeatureFlag;

namespace Console.Controllers.FeatureFlags.Delete;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}