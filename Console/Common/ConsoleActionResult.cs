namespace Console.Common;

public sealed class ConsoleActionResult : IConsoleActionResult
{
    public int ExitCode { get; init; } = (int)Common.ExitCode.Error;

    public IEnumerable<string> Lines { get; init; } = new List<string>
    {
        "Unknown error."
    };
}