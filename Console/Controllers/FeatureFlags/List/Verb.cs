using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

[Verb("featureflag:list", HelpText = "HelpListFeatureFlagsVerb",
    ResourceType = typeof(Resources.CommandLineParser))]
[ReadOnlyVerb]
public sealed class Verb : IHasControllerType
{
    public Type ControllerType { get; } = typeof(Controller);
}