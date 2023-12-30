using Console.Commands.FeatureFlags.Create;
using Console.Common;
using Microsoft.Extensions.DependencyInjection;
using CreateFeatureFlagInteractor = Application.Interactors.CreateFeatureFlag;

namespace Console.Extensions;

public static class PresenterExtensions
{
    public static void AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<IConsoleWriter, ConsoleWriter>();

        services.AddTransient<IConsolePresenter, ConsolePresenter>();
    }
}