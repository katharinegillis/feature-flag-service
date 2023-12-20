namespace Domain.FeatureFlags;

public interface IFactory
{
    public IFeatureFlag Create(string id, bool enabled);
}