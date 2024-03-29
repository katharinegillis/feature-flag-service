using Get = Console.Controllers.FeatureFlags.Get;
using Create = Console.Controllers.FeatureFlags.Create;
using List = Console.Controllers.FeatureFlags.List;
using Update = Console.Controllers.FeatureFlags.Update;
using Delete = Console.Controllers.FeatureFlags.Delete;
using Microsoft.Extensions.DependencyInjection;

namespace Console.Extensions;

public static class ControllerExtensions
{
    public static void AddControllers(this IServiceCollection services)
    {
        services.AddTransient<Create.Controller>();
        services.AddTransient<Get.Controller>();
        services.AddTransient<List.Controller>();
        services.AddTransient<Update.Controller>();
        services.AddTransient<Delete.Controller>();
    }
}