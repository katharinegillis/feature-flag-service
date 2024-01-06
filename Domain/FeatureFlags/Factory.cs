namespace Domain.FeatureFlags;

public sealed class Factory : IFactory
{
    public IModel Create(string id, bool enabled)
    {
        return new Model
        {
            Id = id,
            Enabled = enabled
        };
    }

    public IModel Create()
    {
        return NullModel.Instance;
    }
}