using CommandLine;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

[Verb("featureflag:update", HelpText = "Update a feature flag")]
public class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Option('i', "id", Required = true)] public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false)]
    public bool? Enabled { get; set; }
}