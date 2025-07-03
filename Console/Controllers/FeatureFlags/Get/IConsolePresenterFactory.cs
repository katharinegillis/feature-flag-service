using Application.UseCases.FeatureFlag.Get;

namespace Console.Controllers.FeatureFlags.Get;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}