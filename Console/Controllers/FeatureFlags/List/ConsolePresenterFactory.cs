using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create()
    {
        return new ConsolePresenter(localizer);
    }
}