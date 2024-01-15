using Console.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public sealed class ConsolePresenterFactory(ILocalizationService<ConsolePresenter> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create()
    {
        return new ConsolePresenter(localizer, writer);
    }
}