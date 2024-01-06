using Get = Console.Controllers.FeatureFlags.Get;
using Create = Console.Controllers.FeatureFlags.Create;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class ControllerExtensions
{
    public static void AddControllers(this IServiceCollection services)
    {
        services.AddTransient<Create.Controller>();
        services.AddTransient<Get.Controller>();
    }
}