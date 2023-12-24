namespace Domain.FeatureFlags;

public class Factory : IFactory
{
    public IModel Create(string id, bool enabled)
    {
        return new Model
        {
            Id = id,
            Enabled = enabled
        };
    }
}