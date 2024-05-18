using Microsoft.Extensions.DependencyInjection;
using Create = Application.Interactors.CreateFeatureFlag;
using Get = Application.Interactors.GetFeatureFlag;
using List = Application.Interactors.ListFeatureFlags;
using Update = Application.Interactors.UpdateFeatureFlag;
using Delete = Application.Interactors.DeleteFeatureFlag;

namespace Console.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationInteractors(this IServiceCollection services)
    {
        services.AddScoped<Create.IInputPort, Create.Interactor>();
        services.AddScoped<Get.IInputPort, Get.Interactor>();
        services.AddScoped<List.IInputPort, List.Interactor>();
        services.AddScoped<Update.IInputPort, Update.Interactor>();
        services.AddScoped<Delete.IInputPort, Delete.Interactor>();
    }
}