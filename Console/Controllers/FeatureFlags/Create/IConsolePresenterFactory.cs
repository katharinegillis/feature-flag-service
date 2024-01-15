using Application.Interactors.CreateFeatureFlag;

namespace Console.Controllers.FeatureFlags.Create;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}