using System.Globalization;
using Console;
using Console.Extensions;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("consolesettings.json", optional: true);
builder.Logging.ClearProviders();

builder.Services.AddDomainFactories();

builder.Services.AddApplicationInteractors();

var splitIoOptionsBuilder = builder.Configuration.GetSection(SplitIoOptions.SplitIo);

builder.Services.AddInfrastructureSplitIoConfig(builder.Configuration);

var splitIoOptions = splitIoOptionsBuilder.Get<SplitIoOptions>();

if (splitIoOptions != null && splitIoOptions.SdkKey != "")
{
    try
    {
        builder.Services.AddInfrastructureSplitIoRepositories(splitIoOptions);
    }
    catch (Exception)
    {
        builder.Services.AddInfrastructureSqliteRepositories();
    }
}
else
{
    builder.Services.AddInfrastructureSqliteRepositories();
}

builder.Services.AddConsoleControllers();
builder.Services.AddConsoleLocalization();
builder.Services.AddConsolePresenters();

var cultureName = Environment.GetEnvironmentVariable("CONSOLE_CULTURE") ?? "en-CA";

CultureInfo.CurrentCulture = new CultureInfo(cultureName, false);
CultureInfo.CurrentUICulture = new CultureInfo(cultureName, false);

builder.Services.AddHostedService<App>();
using var host = builder.Build();

await host.RunAsync();