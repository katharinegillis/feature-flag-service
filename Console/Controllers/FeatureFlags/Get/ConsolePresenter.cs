using Console.Common;
using Domain.FeatureFlags;
using Utilities.LocalizationService;

namespace Console.Controllers.FeatureFlags.Get;

public sealed class ConsolePresenter(
    ILocalizationService<ConsolePresenter> localizer,
    IConsoleWriter writer)
    : IConsolePresenter
{
    public void Ok(IModel featureFlag)
    {
        writer.WriteLine(localizer.Translate("Id: \"{0}\", Enabled: \"{1}\"", featureFlag.Id,
            localizer.Translate(featureFlag.Enabled ? "true" : "false")));

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public void NotFound()
    {
        writer.WriteLine("Feature Flag doesn\'t exist.");

        ExitCode = (int)Console.Common.ExitCode.Success;
    }

    public int ExitCode { get; private set; }
}