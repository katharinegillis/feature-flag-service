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

        services.AddScoped<Create.IConsolePresenterFactory, Create.ConsolePresenterFactory>();
        services.AddScoped<Get.IConsolePresenterFactory, Get.ConsolePresenterFactory>();
        services.AddScoped<List.IConsolePresenterFactory, List.ConsolePresenterFactory>();
        services.AddScoped<Update.IConsolePresenterFactory, Update.ConsolePresenterFactory>();
    }
}