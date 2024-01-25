using Application.Interactors.DeleteFeatureFlag;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Delete;

public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<SharedResource> localizer,
    IConsoleWriter writer) : IConsolePresenter
{
    public int ExitCode { get; private set; }

    public void Ok()
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" deleted.", request.Id));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void NotFound()
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", request.Id));

        ExitCode = (int)Console.Common.ExitCode.OptionsError;
    }

    public void Error(Error error)
    {
        writer.WriteLine(localizer.Translate("Error: {0}.", localizer.Translate(error.Message)));

        ExitCode = (int)Console.Common.ExitCode.Error;
    }

    public RequestModel Request => request;
}