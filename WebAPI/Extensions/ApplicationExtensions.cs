using Get = Application.UseCases.FeatureFlag.Get;
using IsEnabled = Application.UseCases.FeatureFlag.IsEnabled;

namespace WebAPI.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationInteractors(this IServiceCollection services)
    {
        services.AddScoped<Get.IUseCase, Get.UseCase>();
        services.AddScoped<IsEnabled.IUseCase, IsEnabled.UseCase>();
    }

    public static void AddApplicationPresenters(this IServiceCollection services)
    {
        // TODO: Move this up to Application library
        services
            .AddScoped<Get.ICodePresenterFactory, Get.CodePresenterFactory>();
    }
}