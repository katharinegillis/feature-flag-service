using Microsoft.Extensions.DependencyInjection;
using Utilities.LocalizationService;

namespace Console.Extensions;

public static class LocalizationExtensions
{
    public static void AddLocalizationServices(this IServiceCollection services)
    {
        services.AddLocalization(options => { options.ResourcesPath = "Resources"; });

        services.AddScoped(typeof(ILocalizationService<>), typeof(StringLocalizerLocalizationService<>));
    }
}