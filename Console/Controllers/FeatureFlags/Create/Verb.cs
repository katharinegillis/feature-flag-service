using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Create;

[Verb("featureflag:create", HelpText = "Create a feature flag")]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true)] public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false)]
    public bool Enabled { get; set; }
}