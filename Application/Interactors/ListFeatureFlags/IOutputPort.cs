using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public interface IOutputPort
{
    public void Ok(IEnumerable<IModel> featureFlags);
}