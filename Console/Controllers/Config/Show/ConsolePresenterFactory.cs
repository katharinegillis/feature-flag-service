using Application.UseCases.Config.Show;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.Config.Show;

public class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer) : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel? request)
    {
        return new ConsolePresenter(request, localizer);
    }
}