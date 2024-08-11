using Domain.FeatureFlags;

namespace Application.Interactors.FeatureFlag.List;

public interface IOutputPort
{
    public void Ok(IEnumerable<IModel> featureFlags);
}