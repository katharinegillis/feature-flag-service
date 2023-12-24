using Microsoft.Extensions.Localization;

namespace Console.Common;

public interface IConsoleWriter
{
    public void WriteLine(string line);

    public void WriteLine(LocalizedString line);
}