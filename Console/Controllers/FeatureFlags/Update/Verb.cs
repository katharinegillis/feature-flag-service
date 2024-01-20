using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

[Verb("featureflag:update", HelpText = "HelpUpdateFeatureFlagVerb",
    ResourceType = typeof(Resources.CommandLineParser))]
public class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption",
        ResourceType = typeof(Resources.CommandLineParser))]
    public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false, HelpText = "HelpEnableOption",
        ResourceType = typeof(Resources.CommandLineParser))]
    public bool? Enabled { get; set; }
}