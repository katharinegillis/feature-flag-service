using CommandLine;
using Console.Common;

namespace Console.Commands.FeatureFlags.Create;

// ReSharper disable once UnusedType.Global
[Verb("featureflag:create", HelpText = "Create a feature flag")]
public class Verb : IHasCommandType, IOptions
{
    public Type CommandType { get; } = typeof(Command);

    [Option('i', "id", Required = true)]
    // ReSharper disable once UnusedMember.Global
    public string Id { get; set; } = null!;

    [Option('e', "enabled", Default = false)]
    // ReSharper disable once UnusedMember.Global
    public bool Enabled { get; set; }
}