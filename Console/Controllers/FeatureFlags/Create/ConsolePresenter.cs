using Application.Interactors.CreateFeatureFlag;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Create;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<SharedResource> localizer,
    IConsoleWriter writer)
    : IConsolePresenter
{
    public void Ok(string id)
    {
        writer.WriteLine(localizer.Translate("Feature Flag \"{0}\" created.", request.Id));

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

    public RequestModel Request => request;

    public int ExitCode { get; private set; }
}