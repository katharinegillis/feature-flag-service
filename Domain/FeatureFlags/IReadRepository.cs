namespace Domain.FeatureFlags;

public interface IReadRepository
{
    // ReSharper disable once UnusedMemberInSuper.Global
    public string Name { get; }
    public Task<IModel> Get(string id);

    public Task<IEnumerable<IModel>> List();
}