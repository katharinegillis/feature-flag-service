using EnabledController = WebAPI.Controllers.Enabled;

namespace WebAPI.Extensions;

public static class PresenterExtensions
{
    public static void AddPresenters(this IServiceCollection services)
    {
        services.AddTransient<EnabledController.IPresenter, EnabledController.ActionResultPresenter>();
    }
}