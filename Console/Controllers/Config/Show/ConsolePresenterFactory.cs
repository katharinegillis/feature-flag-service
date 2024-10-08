using Application.Interactors.Config.Show;

namespace Console.Controllers.Config.Show;

public class ConsolePresenterFactory : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel? request)
    {
        return new ConsolePresenter(request);
    }
}