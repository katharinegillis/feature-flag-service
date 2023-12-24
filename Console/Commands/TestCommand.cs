using Console.Common;
using Console.Verbs;

namespace Console.Commands;

public class TestCommand : IRunnableWithOptions
{
    private TestVerb? _options;

    public void SetOptions(object options)
    {
        _options = options as TestVerb;
    }

    public async Task<int> Run()
    {
        if (_options is null)
        {
            System.Console.WriteLine("Options missing for command.");
            return (int)ExitCode.OptionsError;
        }

        System.Console.WriteLine($"My name is {_options.Name}.");
        await Task.Delay(2000);
        System.Console.WriteLine("I ran!");
        return (int)ExitCode.Success;
    }
}