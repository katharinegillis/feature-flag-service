using Domain.Common;

namespace Domain.FeatureFlags;

public interface IFeatureFlagRepository
{
    public Task<IFeatureFlag> Get(string id);

    public Task<Result<string, Error>> Create(IFeatureFlag featureFlag);
}