using EnabledController = WebAPI.Controllers.Enabled;
using GetFeatureFlagInteractor = Application.Interactors.GetFeatureFlag;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class PresenterExtensions
{
    // ReSharper disable once UnusedMember.Global
    public static void AddPresenters(this IServiceCollection services)
    {
        services
            .AddScoped<GetFeatureFlagInteractor.ICodePresenterFactory, GetFeatureFlagInteractor.CodePresenterFactory>();

        services
            .AddScoped<EnabledController.IActionResultPresenterFactory,
                EnabledController.ActionResultPresenterFactory>();
    }
}