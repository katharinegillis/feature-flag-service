using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Get;

[Verb("featureflag:get", HelpText = "Get a feature flag")]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true)] public string Id { get; set; } = null!;
}