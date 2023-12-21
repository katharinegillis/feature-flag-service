namespace Domain.FeatureFlags;

public interface IFactory
{
    public IModel Create(string id, bool enabled);
}