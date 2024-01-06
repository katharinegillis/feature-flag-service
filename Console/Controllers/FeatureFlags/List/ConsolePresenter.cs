using Console.Common;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.List;

public class ConsolePresenter(ILocalizationService<ConsolePresenter> localizationService, IConsoleWriter writer)
    : IConsolePresenter
{
    public int ExitCode { get; private set; }

    public void Ok(IEnumerable<IModel> featureFlags)
    {
        foreach (var featureFlag in featureFlags)
        {
            writer.WriteLine(localizationService.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
                localizationService.Translate(featureFlag.Enabled ? "true" : "false")));
        }

        ExitCode = (int)Console.Common.ExitCode.Success;
    }
}