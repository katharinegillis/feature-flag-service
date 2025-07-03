using Application.UseCases.FeatureFlag.Delete;
using Console.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Delete;

public sealed class ConsolePresenterFactory(ILocalizationService<SharedResource> localizer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer);
    }
}