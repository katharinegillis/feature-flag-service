using Application.UseCases.FeatureFlag.Create;

namespace Console.Controllers.FeatureFlags.Create;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}