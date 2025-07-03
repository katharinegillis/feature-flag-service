using Microsoft.Extensions.DependencyInjection;
using Create = Application.UseCases.FeatureFlag.Create;
using Get = Application.UseCases.FeatureFlag.Get;
using List = Application.UseCases.FeatureFlag.List;
using Update = Application.UseCases.FeatureFlag.Update;
using Delete = Application.UseCases.FeatureFlag.Delete;
using Show = Application.UseCases.Config.Show;

namespace Console.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationInteractors(this IServiceCollection services)
    {
        services.AddScoped<Create.IUseCase, Create.UseCase>();
        services.AddScoped<Get.IUseCase, Get.UseCase>();
        services.AddScoped<List.IUseCase, List.UseCase>();
        services.AddScoped<Update.IUseCase, Update.UseCase>();
        services.AddScoped<Delete.IUseCase, Delete.UseCase>();
        services.AddScoped<Show.IUseCase, Show.UseCase>();
    }
}