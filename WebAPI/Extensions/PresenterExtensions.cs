using EnabledController = WebAPI.Controllers.Enabled;
using GetFeatureFlagInteractor = Application.Interactors.GetFeatureFlag;
using IsFeatureFlagEnabledInteractor = Application.Interactors.IsFeatureFlagEnabled;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class PresenterExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static void AddPresenters(this IServiceCollection services)
    {
        services.AddTransient<EnabledController.IPresenter, EnabledController.ActionResultPresenter>();
        services.AddTransient<GetFeatureFlagInteractor.ICodePresenter, GetFeatureFlagInteractor.CodePresenter>();
        services
            .AddTransient<IsFeatureFlagEnabledInteractor.ICodePresenter,
                IsFeatureFlagEnabledInteractor.CodePresenter>();
    }
}