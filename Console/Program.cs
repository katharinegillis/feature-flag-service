using System.Globalization;
using Console;
using Console.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("consolesettings.json", optional: true);
builder.Logging.ClearProviders();

builder.Services.AddSqliteServer();
builder.Services.AddRepositories();
builder.Services.AddPresenters();
builder.Services.AddInteractors();
builder.Services.AddControllers();
builder.Services.AddLocalizationServices();

var cultureName = Environment.GetEnvironmentVariable("CONSOLE_CULTURE") ?? "en-CA";

CultureInfo.CurrentCulture = new CultureInfo(cultureName, false);
CultureInfo.CurrentUICulture = new CultureInfo(cultureName, false);

builder.Services.AddHostedService<App>();
using var host = builder.Build();

await host.RunAsync();