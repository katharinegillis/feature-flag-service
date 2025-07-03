using CommandLine;
using Console.Common;
using Console.Resources;

namespace Console.Controllers.Config.Show;

[Verb("config:show", HelpText = "HelpShowConfigVerb", ResourceType = typeof(CommandLineParser))]
[ReadOnlyVerb]
public sealed class Verb : IHasControllerType, IOptions
{
    public Type ControllerType { get; } = typeof(Controller);

    [Value(0, Required = true, HelpText = "HelpShowConfigValue", ResourceType = typeof(CommandLineParser))]
    public string Name { get; set; } = null!;
}