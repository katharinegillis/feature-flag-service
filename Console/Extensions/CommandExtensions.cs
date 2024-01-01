using Get = Console.Commands.FeatureFlags.Get;
using Create = Console.Commands.FeatureFlags.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class CommandExtensions
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddTransient<Create.Command>();
        services.AddTransient<Get.Command>();
    }
}