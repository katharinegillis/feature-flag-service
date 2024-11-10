using Application.Interactors.FeatureFlag.Get;
using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<SharedResource> localizer,
    IConsoleWriter writer)
    : IConsolePresenter
{
    public void Ok(IModel featureFlag)
    {
        writer.WriteLine(localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
            localizer.Translate(featureFlag.Enabled ? "true" : "false")));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void NotFound()
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", request.Id));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public RequestModel Request => request;

    public int ExitCode { get; private set; }
}