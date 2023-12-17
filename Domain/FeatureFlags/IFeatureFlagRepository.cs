namespace Domain.FeatureFlags;

public interface IFeatureFlagRepository
{
    public Task<IFeatureFlag> Get(string id);
}