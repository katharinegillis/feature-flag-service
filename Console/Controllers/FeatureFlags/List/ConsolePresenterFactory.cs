using Console.Common;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create()
    {
        return new ConsolePresenter(localizer, writer);
    }
}