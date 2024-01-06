using Microsoft.Extensions.DependencyInjection;
using Create = Application.Interactors.CreateFeatureFlag;
using Get = Application.Interactors.GetFeatureFlag;
using List = Application.Interactors.ListFeatureFlags;

namespace Console.Extensions;

public static class InteractorExtensions
{
    public static void AddInteractors(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
        services.AddScoped<Create.IInputPort, Create.Interactor>();
        services.AddScoped<Get.IInputPort, Get.Interactor>();
        services.AddScoped<List.IInputPort, List.Interactor>();
    }
}