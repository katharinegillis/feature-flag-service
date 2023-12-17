using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public interface IOutputPort
{
    public void Ok(IFeatureFlag featureFlag);

    public void NotFound();
}