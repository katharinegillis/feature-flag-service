using Console.Commands.FeatureFlags.Create;
using Microsoft.Extensions.DependencyInjection;
using CreateFeatureFlagInteractors = Application.Interactors.CreateFeatureFlag;

namespace Console.Extensions;

public static class CommandExtensions
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddTransient<Command>();
    }
}