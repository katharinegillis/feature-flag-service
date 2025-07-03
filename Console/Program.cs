using System.Globalization;
using Console;
using Console.Extensions;
using Infrastructure.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("consolesettings.json", true);
builder.Logging.ClearProviders();

builder.Services.AddDomainFactories();

builder.Services.AddApplicationInteractors();

var splitOptionsBuilder = builder.Configuration.GetSection(SplitOptions.Split);

builder.Services.AddInfrastructureSplitConfig(builder.Configuration);

var splitOptions = splitOptionsBuilder.Get<SplitOptions>();

if (splitOptions != null && splitOptions.SdkKey != "")
    try
    {
        builder.Services.AddInfrastructureSplitRepository(splitOptions);
    }
    catch (Exception)
    {
        builder.Services.AddInfrastructureSqliteRepository();
    }
else
    builder.Services.AddInfrastructureSqliteRepository();

builder.Services.AddConsoleControllers();
builder.Services.AddConsoleLocalization();
builder.Services.AddConsolePresenters();

var cultureName = Environment.GetEnvironmentVariable("CONSOLE_CULTURE") ?? "en-CA";

CultureInfo.CurrentCulture = new CultureInfo(cultureName, false);
CultureInfo.CurrentUICulture = new CultureInfo(cultureName, false);

builder.Services.AddHostedService<App>();
using var host = builder.Build();

await host.RunAsync();