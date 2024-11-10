using Application.Interactors.Config.Show;
using Console.Common;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.Config.Show;

public class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer, IConsoleWriter writer) : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel? request)
    {
        return new ConsolePresenter(request, localizer, writer);
    }
}