using IsFeatureFlagEnabledInteractor = Application.Interactors.IsFeatureFlagEnabled;
using GetFeatureFlagInteractor = Application.Interactors.GetFeatureFlag;

namespace WebAPI.Extensions;

public static class ApplicationExtensions
{
    public static void AddApplicationInteractors(this IServiceCollection services)
    {
        services.AddScoped<GetFeatureFlagInteractor.IInputPort, GetFeatureFlagInteractor.Interactor>();
        services.AddScoped<IsFeatureFlagEnabledInteractor.IInputPort, IsFeatureFlagEnabledInteractor.Interactor>();
    }

    public static void AddApplicationPresenters(this IServiceCollection services)
    {
        services
            .AddScoped<GetFeatureFlagInteractor.ICodePresenterFactory, GetFeatureFlagInteractor.CodePresenterFactory>();
    }
}