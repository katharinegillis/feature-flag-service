using Get = Console.Commands.FeatureFlags.Get;
using Create = Console.Commands.FeatureFlags.Create;
using Console.Common;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class PresenterExtensions
{
    public static void AddPresenters(this IServiceCollection services)
    {
        services.AddScoped<IConsoleWriter, ConsoleWriter>();

        services.AddTransient<Create.IConsolePresenter, Create.ConsolePresenter>();
        services.AddTransient<Get.IConsolePresenter, Get.ConsolePresenter>();
    }
}