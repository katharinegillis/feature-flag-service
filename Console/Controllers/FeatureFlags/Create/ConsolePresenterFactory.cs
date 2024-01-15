using Application.Interactors.CreateFeatureFlag;
using Console.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Create;

public sealed class ConsolePresenterFactory(ILocalizationService<ConsolePresenter> localizer, IConsoleWriter writer)
    : IConsolePresenterFactory
{
    public IConsolePresenter Create(RequestModel request)
    {
        return new ConsolePresenter(request, localizer, writer);
    }
}