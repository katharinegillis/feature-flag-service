using Get = Application.Interactors.FeatureFlag.Get;
using IsEnabled = Application.Interactors.FeatureFlag.IsEnabled;

namespace WebAPI.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationInteractors(this IServiceCollection services)
    {
        services.AddScoped<Get.IInputPort, Get.Interactor>();
        services.AddScoped<IsEnabled.IInputPort, IsEnabled.Interactor>();
    }

    public static void AddApplicationPresenters(this IServiceCollection services)
    {
        // TODO: Move this up to Application library
        services
            .AddScoped<Get.ICodePresenterFactory, Get.CodePresenterFactory>();
    }
}