using CommandLine;
using Console.Common;

namespace Console.Commands.FeatureFlags.Get;

[Verb("featureflag:get", HelpText = "Get a feature flag")]
public class Verb : IHasCommandType, IOptions
{
    public Type CommandType { get; } = typeof(Command);

    [Option('i', "id", Required = true)] public string Id { get; set; } = null!;
}