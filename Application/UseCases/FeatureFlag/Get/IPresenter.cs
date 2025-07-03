using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.Get;

public interface IPresenter
{
    public void Ok(IModel featureFlag);

    public void NotFound();
}