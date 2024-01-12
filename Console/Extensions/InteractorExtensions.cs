using Microsoft.Extensions.DependencyInjection;
using Create = Application.Interactors.CreateFeatureFlag;
using Get = Application.Interactors.GetFeatureFlag;
using List = Application.Interactors.ListFeatureFlags;
using Update = Application.Interactors.UpdateFeatureFlag;

namespace Console.Extensions;

public static class InteractorExtensions
{
    public static void AddInteractors(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
        services.AddScoped<Create.IInputPort, Create.Interactor>();
        services.AddScoped<Get.IInputPort, Get.Interactor>();
        services.AddScoped<List.IInputPort, List.Interactor>();
        services.AddScoped<Update.IInputPort, Update.Interactor>();
    }
}