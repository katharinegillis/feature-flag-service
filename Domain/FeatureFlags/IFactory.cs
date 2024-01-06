namespace Domain.FeatureFlags;

public interface IFactory
{
    public IModel Create(string id, bool enabled);

    // ReSharper disable once UnusedMemberInSuper.Global
    public IModel Create();
}