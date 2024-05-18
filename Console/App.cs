using System.Reflection;
using CommandLine;
using CommandLine.Text;
using Console.Common;
using Console.Localization;
using Domain.FeatureFlags;
using Microsoft.Extensions.Hosting;
using Utilities.LocalizationService;

namespace Console;

public sealed class App(
    IHostApplicationLifetime applicationLifetime,
    IServiceProvider serviceProvider,
    ILocalizationService<App> localizer) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        SentenceBuilder.Factory = () => new LocalizableSentenceBuilder();

        var args = Environment.GetCommandLineArgs().Skip(1);

        var types = LoadVerbs(serviceProvider);

        await Parser.Default.ParseArguments(args, types)
            .WithParsedAsync(RunAsync);

        applicationLifetime.StopApplication();
    }

    private static Type[] LoadVerbs(IServiceProvider serviceProvider)
    {
        var isReadOnlyRepository = serviceProvider.GetService(typeof(IRepository)) == null;
        return Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.GetCustomAttribute<VerbAttribute>() != null &&
                        (!isReadOnlyRepository || t.GetCustomAttribute<ReadOnlyVerbAttribute>() != null)).ToArray();
    }

    private async Task RunAsync(object obj)
    {
        var verb = (IHasControllerType)obj;

        if (serviceProvider.GetService(verb.ControllerType) is not IExecutable command)
        {
            System.Console.WriteLine(localizer.Translate("Unknown command."));
            return;
        }

        if (command is IHasOptions commandWithOptions)
        {
            commandWithOptions.SetOptions(obj);
        }

        Environment.ExitCode = await command.Execute();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}