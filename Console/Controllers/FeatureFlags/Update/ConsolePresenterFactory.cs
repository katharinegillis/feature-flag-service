using Application.Interactors.UpdateFeatureFlag;
using Console.Common;
using Microsoft.Extensions.Localization;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Update;

public class ConsolePresenterFactory(ILocalizationService<ConsolePresenter> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer, writer);
    }
}