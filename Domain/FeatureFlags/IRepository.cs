using Domain.Common;

namespace Domain.FeatureFlags;

public interface IRepository
{
    public Task<IModel> Get(string id);

    public Task<Result<string, Error>> Create(IModel model);

    public Task<IEnumerable<IModel>> List();
}