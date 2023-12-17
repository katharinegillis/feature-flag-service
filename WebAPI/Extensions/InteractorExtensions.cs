using IsFeatureFlagEnabledInteractors = Application.Interactors.IsFeatureFlagEnabled;
using GetFeatureFlagInteractors = Application.Interactors.GetFeatureFlag;

namespace WebAPI.Extensions;

public static class InteractorExtensions
{
    public static void AddInteractors(this IServiceCollection services)
    {
        services.AddScoped<GetFeatureFlagInteractors.IInputPort, GetFeatureFlagInteractors.Interactor>();
        services.AddScoped<IsFeatureFlagEnabledInteractors.IInputPort, IsFeatureFlagEnabledInteractors.Interactor>();
    }
}