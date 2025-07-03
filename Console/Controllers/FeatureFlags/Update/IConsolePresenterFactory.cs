using Application.UseCases.FeatureFlag.Update;

namespace Console.Controllers.FeatureFlags.Update;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request);
}