using Microsoft.Extensions.DependencyInjection;
using Create = Application.Interactors.FeatureFlag.Create;
using Get = Application.Interactors.FeatureFlag.Get;
using List = Application.Interactors.FeatureFlag.List;
using Update = Application.Interactors.FeatureFlag.Update;
using Delete = Application.Interactors.FeatureFlag.Delete;
using Show = Application.Interactors.Config.Show;

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
        services.AddScoped<Show.IInputPort, Show.Interactor>();
    }
}