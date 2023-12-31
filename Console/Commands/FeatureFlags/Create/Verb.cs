using CommandLine;
using Console.Common;

namespace Console.Commands.FeatureFlags.Create;

[Verb("featureflag:create", HelpText = "Create a feature flag")]
public class Verb : IHasCommandType, IOptions
{
    public Type CommandType { get; } = typeof(Command);

    [Option('i', "id", Required = true)] public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false)]
    public bool Enabled { get; set; }
}