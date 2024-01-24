using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Delete;

[Verb("featureflag:delete", HelpText = "HelpDeleteFeatureFlagVerb", ResourceType = typeof(Resources.CommandLineParser))]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption", ResourceType = typeof(Resources.CommandLineParser))]
    public string Id { get; set; }
}