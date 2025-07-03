using Application.UseCases.FeatureFlag.Update;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Update;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer);
    }
}