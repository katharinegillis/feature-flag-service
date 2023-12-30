using Console.Common;
using Domain.Common;
using Microsoft.Extensions.Localization;

namespace Console.Commands.FeatureFlags.Create;

public class ConsolePresenter(IStringLocalizer<ConsolePresenter> localizer, IConsoleWriter consoleWriter)
    : IConsolePresenter
{
    public void Ok(string id)
    {
        consoleWriter.WriteLine(localizer["Feature Flag \"{0}\" created.", id]);

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        foreach (var error in validationErrors)
        {
            consoleWriter.WriteLine(localizer["{0}: {1}.", error.Field, localizer[error.Message]]);
        }

        ExitCode = (int)Console.Common.ExitCode.OptionsError;
    }

    public void Error(Error error)
    {
        consoleWriter.WriteLine(localizer["Error: {0}.", localizer[error.Message]]);

        ExitCode = (int)Console.Common.ExitCode.Error;
    }

    public int ExitCode { get; set; }
}