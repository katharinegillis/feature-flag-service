using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public sealed class ConsolePresenter(ILocalizationService<SharedResource> localizer)
    : IConsolePresenter
{
    public void Ok(IEnumerable<IModel> featureFlags)
    {
        var lines = featureFlags.Select(featureFlag => localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"",
            featureFlag.Id, localizer.Translate(featureFlag.Enabled ? "true" : "false"))).ToList();

        ActionResult = new ConsoleActionResult
        {
            Lines = lines,
            ExitCode = (int)ExitCode.Success
        };
    }

    public IConsoleActionResult ActionResult { get; private set; } = new ConsoleActionResult();
}