using Console.Common;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Create;

// ReSharper disable once SuggestBaseTypeForParameterInConstructor
public class ConsolePresenter(
    ILocalizationService<ConsolePresenter> localizer,
    IConsoleWriter consoleWriter)
    : IConsolePresenter
{
    public void Ok(string id)
    {
        consoleWriter.WriteLine(localizer.Translate("Feature Flag \"{0}\" created.", id));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        foreach (var error in validationErrors)
        {
            consoleWriter.WriteLine(localizer.Translate("{0}: {1}.", error.Field, localizer.Translate(error.Message)));
        }

        ExitCode = (int)Console.Common.ExitCode.OptionsError;
    }

    public void Error(Error error)
    {
        consoleWriter.WriteLine(localizer.Translate("Error: {0}.", localizer.Translate(error.Message)));

        ExitCode = (int)Console.Common.ExitCode.Error;
    }

    public int ExitCode { get; private set; }
}