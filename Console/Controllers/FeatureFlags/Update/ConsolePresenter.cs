using Application.UseCases.FeatureFlag.Update;
using Console.Common;
using Console.Localization;
using Domain.Common;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Update;

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
                localizer.Translate("Feature Flag \"{0}\" updated.", request.Id)
            },
            ExitCode = (int)ExitCode.Success
        };
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        var lines = validationErrors
            .Select(error => localizer.Translate("{0}: {1}.", error.Field, localizer.Translate(error.Message)))
            .ToList();

        ActionResult = new ConsoleActionResult
        {
            Lines = lines,
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

    public void NotFound()
    {
        ActionResult = new ConsoleActionResult
        {
            Lines = new List<string>
            {
                localizer.Translate("Feature Flag \"{0}\" doesn\'t exist.", request.Id)
            },
            ExitCode = (int)ExitCode.OptionsError
        };
    }

    public RequestModel Request => request;

    public IConsoleActionResult ActionResult { get; private set; } = new ConsoleActionResult();
}