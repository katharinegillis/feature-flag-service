using Domain.FeatureFlags;
using Infrastructure.Configuration;
using Microsoft.Extensions.Options;
using Splitio.Services.Client.Interfaces;

namespace Infrastructure.Persistence.Repositories;

public sealed class SplitFeatureFlagRepository(
    ISplitFactory splitFactory,
    IOptions<SplitOptions> options,
    IFactory factory)
    : IReadRepository
{
    private readonly SplitOptions _options = options.Value;
    private readonly ISplitClient _splitClient = splitFactory.Client();
    private readonly ISplitManager _splitManager = splitFactory.Manager();

    public Task<IModel> Get(string id)
    {
        var treatment = _splitClient.GetTreatment(_options.TreatmentKey, id);
        return treatment switch
        {
            "on" => Task.FromResult(factory.Create(id, true)),
            "off" => Task.FromResult(factory.Create(id, false)),
            _ => Task.FromResult(factory.Create())
        };
    }

    public async Task<IEnumerable<IModel>> List()
    {
        var splitNames = await _splitManager.SplitNamesAsync();
        var treatments = _splitClient.GetTreatments(_options.TreatmentKey, splitNames);
        return treatments.Select(treatment => treatment.Value switch
        {
            "on" => factory.Create(treatment.Key, true),
            "off" => factory.Create(treatment.Key, false),
            _ => factory.Create()
        });
    }

    public string Name => "Split";
}