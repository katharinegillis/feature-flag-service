using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public sealed class ConsolePresenter(ILocalizationService<SharedResource> localizer, IConsoleWriter writer)
    : IConsolePresenter
{
    public int ExitCode { get; private set; }

    public void Ok(IEnumerable<IModel> featureFlags)
    {
        foreach (var featureFlag in featureFlags)
        {
            writer.WriteLine(localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
                localizer.Translate(featureFlag.Enabled ? "true" : "false")));
        }

        ExitCode = (int)Console.Common.ExitCode.Success;
    }
}