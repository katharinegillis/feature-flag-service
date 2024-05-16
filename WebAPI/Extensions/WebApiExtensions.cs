using EnabledController = WebAPI.Controllers.Enabled;

namespace WebAPI.Extensions;

public static class WebApiExtensions
{
    public static void AddWebApiPresenters(this IServiceCollection services)
    {
        services
            .AddScoped<EnabledController.IActionResultPresenterFactory,
                EnabledController.ActionResultPresenterFactory>();
    }
}