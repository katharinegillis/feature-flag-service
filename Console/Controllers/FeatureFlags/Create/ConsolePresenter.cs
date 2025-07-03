using Application.UseCases.FeatureFlag.Create;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Create;

public sealed class ConsolePresenter(
    RequestModel request,
    ILocalizationService<SharedResource> localizer)
    : IConsolePresenter
{
    public void Ok()
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Feature Flag \"{0}\" created.", request.Id)
            },
            ExitCode = (int)ExitCode.Success
        };
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = validationErrors.Select(error =>
                localizer.Translate("{0}: {1}", error.Field, localizer.Translate(error.Message))),
            ExitCode = (int)ExitCode.OptionsError
        };
    }

    public void Error(Error error)
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Error: {0}.", localizer.Translate(error.Message))
            },
            ExitCode = (int)ExitCode.Error
        };
    }

    public RequestModel Request => request;

    public IConsoleActionResult ActionResult { get; private set; } = new ConsoleActionResult();
}