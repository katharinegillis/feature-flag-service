using CommandLine;
using Console.Common;
using Console.Resources;

namespace Console.Controllers.FeatureFlags.Delete;

[Verb("featureflag:delete", HelpText = "HelpDeleteFeatureFlagVerb", ResourceType = typeof(CommandLineParser))]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption", ResourceType = typeof(CommandLineParser))]
    public string Id { get; set; } = null!;
}