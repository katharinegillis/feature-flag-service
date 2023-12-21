using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public interface IOutputPort
{
    public void Ok(IModel featureFlag);

    public void NotFound();
}