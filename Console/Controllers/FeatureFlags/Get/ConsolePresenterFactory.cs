using Application.UseCases.FeatureFlag.Get;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer);
    }
}