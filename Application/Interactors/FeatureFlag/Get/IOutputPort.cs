using Domain.FeatureFlags;

namespace Application.Interactors.FeatureFlag.Get;

public interface IOutputPort
{
    public void Ok(IModel featureFlag);

    public void NotFound();
}