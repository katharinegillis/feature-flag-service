namespace Domain.FeatureFlags;

public interface IReadRepository
{
    public Task<IModel> Get(string id);

    public Task<IEnumerable<IModel>> List();

    public string Name { get; }
}