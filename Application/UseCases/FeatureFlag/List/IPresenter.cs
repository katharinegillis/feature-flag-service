using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.List;

public interface IPresenter
{
    public void Ok(IEnumerable<IModel> featureFlags);
}