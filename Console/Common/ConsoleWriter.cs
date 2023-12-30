using Microsoft.Extensions.Localization;

namespace Console.Common;

public class ConsoleWriter : IConsoleWriter
{
    public void WriteLine(string line)
    {
        System.Console.WriteLine(line);
    }

    public void WriteLine(LocalizedString line)
    {
        System.Console.WriteLine(line);
    }
}