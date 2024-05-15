using Infrastructure.Configuration;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Splitio.Services.Client.Classes;
using Splitio.Services.Client.Interfaces;
using Repositories = Infrastructure.Persistence.Repositories;

namespace WebAPI.Extensions;

// ReSharper disable once UnusedType.Global
public static class RepositoryExtensions
{
    private static void AddCommon(this IServiceCollection services)
    {
        services.AddScoped<Domain.FeatureFlags.IFactory, Domain.FeatureFlags.Factory>();
    }
    
    public static void AddSqliteRepositories(this IServiceCollection services)
    {
        AddCommon(services);
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        services.AddDbContext<FeatureFlagContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("FeatureFlagContext")));
        
        services.AddScoped<Domain.FeatureFlags.IRepository, Repositories.DbFeatureFlagRepository>();
        services.AddScoped<Domain.FeatureFlags.IReadRepository, Repositories.DbFeatureFlagRepository>();
    }

    public static void AddSplitIoRepositories(this IServiceCollection services, string splitioSdkKey, string splitioTreatmentKey)
    {
        AddCommon(services);
        
        var splitIoOptions = new SplitIoOptions
        {
            SdkKey = splitioSdkKey,
            TreatmentKey = splitioTreatmentKey
            
        };
        services.AddSingleton(typeof(ISplitIoOptions), splitIoOptions);

        var config = new ConfigurationOptions();
        var factory = new SplitFactory(splitioSdkKey, config);
        var sdk = factory.Client();
        
        sdk.BlockUntilReady(10000);

        services.AddSingleton(typeof(ISplitFactory), factory);

        services.AddScoped<Domain.FeatureFlags.IReadRepository, Repositories.SplitIoFeatureFlagRepository>();
    }
}