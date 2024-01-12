using Get = Console.Controllers.FeatureFlags.Get;
using Create = Console.Controllers.FeatureFlags.Create;
using List = Console.Controllers.FeatureFlags.List;
using Update = Console.Controllers.FeatureFlags.Update;
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
        services.AddTransient<List.IConsolePresenter, List.ConsolePresenter>();
        services.AddTransient<Update.IConsolePresenter, Update.ConsolePresenter>();
    }
}