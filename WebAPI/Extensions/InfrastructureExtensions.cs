using Infrastructure.Configuration;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Splitio.Services.Client.Classes;
using Splitio.Services.Client.Interfaces;
using Repositories = Infrastructure.Persistence.Repositories;

namespace WebAPI.Extensions;

public static class InfrastructureExtensions
{
    public static void AddInfrastructureSqliteRepositories(this IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<FeatureFlagContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("FeatureFlagContext")));
        
        services.AddScoped<Domain.FeatureFlags.IRepository, Repositories.DbFeatureFlagRepository>();
        services.AddScoped<Domain.FeatureFlags.IReadRepository, Repositories.DbFeatureFlagRepository>();
    }

    public static void AddInfrastructureSplitIoRepositories(this IServiceCollection services, SplitIoOptions splitIoOptions)
    {
        var config = new ConfigurationOptions();
        var factory = new SplitFactory(splitIoOptions.SdkKey, config);
        var sdk = factory.Client();
        
        sdk.BlockUntilReady(10000);

        services.AddSingleton(typeof(ISplitFactory), factory);

        services.AddScoped<Domain.FeatureFlags.IReadRepository, Repositories.SplitIoFeatureFlagRepository>();
    }

    public static void AddInfrastructureSplitIoConfig(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<SplitIoOptions>(config.GetSection(SplitIoOptions.SplitIo));
    }
}