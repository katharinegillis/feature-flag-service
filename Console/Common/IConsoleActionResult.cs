namespace Console.Common;

public interface IConsoleActionResult
{
    public int ExitCode { get; }
    public IEnumerable<string> Lines { get; }
}