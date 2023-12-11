namespace Domain.FeatureFlag;

public interface IFeatureFlagRepository
{
    public Task<IFeatureFlag> Get(string id);
}