using CommandLine;
using Console.Common;
using Console.Resources;

namespace Console.Controllers.FeatureFlags.List;

[Verb("featureflag:list", HelpText = "HelpListFeatureFlagsVerb",
    ResourceType = typeof(CommandLineParser))]
[ReadOnlyVerb]
public sealed class Verb : IHasControllerType
{
    public Type ControllerType { get; } = typeof(Controller);
}