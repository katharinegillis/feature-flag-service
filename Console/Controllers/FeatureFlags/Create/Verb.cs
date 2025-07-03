using CommandLine;
using Console.Common;
using Console.Resources;

namespace Console.Controllers.FeatureFlags.Create;

[Verb("featureflag:create", HelpText = "HelpCreateFeatureFlagVerb",
    ResourceType = typeof(CommandLineParser))]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption",
        ResourceType = typeof(CommandLineParser))]
    public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false, HelpText = "HelpEnableOption",
        ResourceType = typeof(CommandLineParser))]
    public bool? Enabled { get; set; }
}