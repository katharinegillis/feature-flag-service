using CommandLine;
using Console.Commands;
using Console.Common;

namespace Console.Verbs;

// ReSharper disable once UnusedType.Global
// ReSharper disable once ClassNeverInstantiated.Global
[Verb("test", HelpText = "Test verb!")]
public class TestVerb : IHasCommandType
{
    // ReSharper disable once MemberCanBePrivate.Global
    // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
    [Option('n', "name", Required = true)] public string Name { get; set; } = null!;

    public Type CommandType { get; } = typeof(TestCommand);
}