using Application.Interactors.Config.Show;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.Config.Show;

public class ConsolePresenter(
    RequestModel? request,
    ILocalizationService<SharedResource> localizer,
    IConsoleWriter writer) : IConsolePresenter
{
    public void Ok(string value)
    {
        writer.WriteLine(localizer.Translate("Datasource \"{0}\"", value));
        ExitCode = (int)Common.ExitCode.Success;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        foreach (var error in validationErrors)
        {
            writer.WriteLine(localizer.Translate("{0}: {1}.", error.Field, localizer.Translate(error.Message)));
        }
        
        ExitCode = (int)Common.ExitCode.OptionsError;
    }

    public int ExitCode { get; private set; }
    public RequestModel? Request => request;
}