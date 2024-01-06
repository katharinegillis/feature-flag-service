using IsFeatureFlagEnabledInteractors = Application.Interactors.IsFeatureFlagEnabled;
using GetFeatureFlagInteractors = Application.Interactors.GetFeatureFlag;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class InteractorExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static void AddInteractors(this IServiceCollection services)
    {
        services.AddScoped<GetFeatureFlagInteractors.IInputPort, GetFeatureFlagInteractors.Interactor>();
        services.AddScoped<IsFeatureFlagEnabledInteractors.IInputPort, IsFeatureFlagEnabledInteractors.Interactor>();
    }
}