using System.Reflection;
using CommandLine;
using Console.Common;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Console;

public class App(IHostApplicationLifetime applicationLifetime, IServiceProvider serviceProvider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var args = Environment.GetCommandLineArgs().Skip(1);

        var types = LoadVerbs();

        await Parser.Default.ParseArguments(args, types)
            .WithParsedAsync(RunAsync);

        applicationLifetime.StopApplication();
    }

    private Type[] LoadVerbs()
    {
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null).ToArray();
    }

    private async Task RunAsync(object obj)
    {
        var verb = (IHasCommandType)obj;


        if (serviceProvider.GetService(verb.CommandType) is not IRunnableWithOptions command)
        {
            System.Console.WriteLine("Verb is missing command.");
            return;
        }

        command.SetOptions(obj);

        Environment.ExitCode = await command.Run();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}