using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

[Verb("featureflag:list", HelpText = "List all feature flags")]
public sealed class Verb : IHasControllerType
{
    public Type ControllerType { get; } = typeof(Controller);
}