using Application.UseCases.Config.Show;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.Config.Show;

public class ConsolePresenter(
    RequestModel? request,
    ILocalizationService<SharedResource> localizer) : IConsolePresenter
{
    public void Ok(string value)
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Datasource \"{0}\"", value)
            },
            ExitCode = (int)ExitCode.Success
        };
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = validationErrors.Select(error =>
                localizer.Translate("{0}: {1}.", error.Field, localizer.Translate(error.Message))).ToList(),
            ExitCode = (int)ExitCode.OptionsError
        };
    }

    public RequestModel? Request => request;

    public IConsoleActionResult ActionResult { get; private set; } = new ConsoleActionResult();
}