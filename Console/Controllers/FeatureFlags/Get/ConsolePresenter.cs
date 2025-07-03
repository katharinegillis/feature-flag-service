using Application.UseCases.FeatureFlag.Get;
using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<SharedResource> localizer)
    : IConsolePresenter
{
    public void Ok(IModel featureFlag)
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
                    localizer.Translate(featureFlag.Enabled ? "true" : "false"))
            },
            ExitCode = (int)ExitCode.Success
        };
    }

    public void NotFound()
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", request.Id)
            },
            ExitCode = (int)ExitCode.Success
        };
    }

    public RequestModel Request => request;

    public IConsoleActionResult ActionResult { get; private set; } = new ConsoleActionResult();
}