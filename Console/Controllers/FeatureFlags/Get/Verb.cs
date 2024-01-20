using CommandLine;
using Console.Common;
using Console.Localization;

namespace Console.Controllers.FeatureFlags.Get;

[Verb("featureflag:get", HelpText = "HelpGetFeatureFlagVerb",
    ResourceType = typeof(Resources.CommandLineParser))]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true, HelpText = "HelpIdOption",
        ResourceType = typeof(Resources.CommandLineParser))]
    public string Id { get; set; } = null!;
}