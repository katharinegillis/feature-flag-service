using Console;
using Console.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: true);
builder.Logging.ClearProviders();

builder.Services.AddTransient<TestCommand>();
builder.Services.AddHostedService<App>();
using var host = builder.Build();

await host.RunAsync();