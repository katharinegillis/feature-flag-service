using Application.Interactors.GetFeatureFlag;
using Console.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenterFactory(ILocalizationService<ConsolePresenter> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer, writer);
    }
}