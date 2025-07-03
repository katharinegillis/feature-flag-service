using Application.UseCases.Config.Show;

namespace Console.Controllers.Config.Show;

public interface IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel? request);
}