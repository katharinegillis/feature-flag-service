using Application.UseCases.FeatureFlag.Create;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Create;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer);
    }
}