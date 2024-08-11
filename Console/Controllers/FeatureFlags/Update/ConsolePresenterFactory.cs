using Application.Interactors.FeatureFlag.Update;
using Console.Common;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Update;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer, writer);
    }
}