using Console.Common;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public class ConsolePresenter(ILocalizationService<ConsolePresenter> localizationService, IConsoleWriter consoleWriter)
    : IConsolePresenter
{
    public void Ok(IModel featureFlag)
    {
        consoleWriter.WriteLine(localizationService.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
            localizationService.Translate(featureFlag.Enabled ? "true" : "false")));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void NotFound()
    {
        consoleWriter.WriteLine("Feature Flag doesn\'t exist.");

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public int ExitCode { get; private set; }
}