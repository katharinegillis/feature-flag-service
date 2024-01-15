using Application.Interactors.UpdateFeatureFlag;
using Console.Common;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Update;

public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<ConsolePresenter> localizer,
    IConsoleWriter writer)
    : IConsolePresenter
{
    public void Ok()
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" updated.", request.Id));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        foreach (var error in validationErrors)
        {
            writer.WriteLine(localizer.Translate("{0}: {1}.", error.Field, localizer.Translate(error.Message)));
        }

        ExitCode = (int)Console.Common.ExitCode.OptionsError;
    }

    public void Error(Error error)
    {
        writer.WriteLine(localizer.Translate("Error: {0}.", localizer.Translate(error.Message)));

        ExitCode = (int)Console.Common.ExitCode.Error;
    }

    public void NotFound()
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", request.Id));

        ExitCode = (int)Console.Common.ExitCode.OptionsError;
    }

    public RequestModel Request => request;

    public int ExitCode { get; private set; }
}