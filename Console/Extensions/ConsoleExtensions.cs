using Microsoft.Extensions.DependencyInjection;
using Utilities.LocalizationService;
using Get = Console.Controllers.FeatureFlags.Get;
using Create = Console.Controllers.FeatureFlags.Create;
using List = Console.Controllers.FeatureFlags.List;
using Update = Console.Controllers.FeatureFlags.Update;
using Delete = Console.Controllers.FeatureFlags.Delete;
using Show = Console.Controllers.Config.Show;

namespace Console.Extensions;

public static class ConsoleExtensions
{
    public static void AddConsoleControllers(this IServiceCollection services)
    {
        services.AddTransient<Create.Controller>();
        services.AddTransient<Get.Controller>();
        services.AddTransient<List.Controller>();
        services.AddTransient<Update.Controller>();
        services.AddTransient<Delete.Controller>();
        services.AddTransient<Show.Controller>();
    }

    public static void AddConsoleLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

        services.AddScoped(typeof(ILocalizationService<>), typeof(StringLocalizerLocalizationService<>));
    }

    public static void AddConsolePresenters(this IServiceCollection services)
    {
        services.AddScoped<Create.IConsolePresenterFactory, Create.ConsolePresenterFactory>();
        services.AddScoped<Get.IConsolePresenterFactory, Get.ConsolePresenterFactory>();
        services.AddScoped<List.IConsolePresenterFactory, List.ConsolePresenterFactory>();
        services.AddScoped<Update.IConsolePresenterFactory, Update.ConsolePresenterFactory>();
        services.AddScoped<Delete.IConsolePresenterFactory, Delete.ConsolePresenterFactory>();
        services.AddScoped<Show.IConsolePresenterFactory, Show.ConsolePresenterFactory>();
    }
}