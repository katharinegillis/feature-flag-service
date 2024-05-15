using Domain.Common;

namespace Domain.FeatureFlags;

public interface IReadRepository
{
    public Task<IModel> Get(string id);

    public Task<IEnumerable<IModel>> List();
}