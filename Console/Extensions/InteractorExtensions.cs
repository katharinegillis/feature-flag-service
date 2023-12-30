using Microsoft.Extensions.DependencyInjection;
using CreateFeatureFlagInteractors = Application.Interactors.CreateFeatureFlag;

namespace Console.Extensions;

public static class InteractorExtensions
{
    public static void AddInteractors(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
        services.AddScoped<CreateFeatureFlagInteractors.IInputPort, CreateFeatureFlagInteractors.Interactor>();
    }
}