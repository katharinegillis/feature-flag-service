using Domain.Common;

namespace Domain.FeatureFlags;

public interface IRepository : IReadRepository
{
    public Task<Result<string, Error>> Create(IModel model);

    public Task<Result<bool, Error>> Update(IModel model);

    public Task<Result<bool, Error>> Delete(string id);
}