using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Create;

[Verb("featureflag:create", HelpText = "HelpCreateFeatureFlagVerb",
    ResourceType = typeof(Resources.CommandLineParser))]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption",
        ResourceType = typeof(Resources.CommandLineParser))]
    public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false, HelpText = "HelpEnableOption",
        ResourceType = typeof(Resources.CommandLineParser))]
    public bool? Enabled { get; set; }
}