namespace Console.Common;

public sealed class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string line)
    {
        System.Console.WriteLine(line);
    }
}