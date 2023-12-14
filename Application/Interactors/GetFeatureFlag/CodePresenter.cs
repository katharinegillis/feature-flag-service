using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public sealed class CodePresenter : IOutputPort
{
    public IFeatureFlag FeatureFlag { get; private set; } = new FeatureFlagNull();
    public bool IsNotFound;

    public void Ok(IFeatureFlag featureFlag)
    {
        FeatureFlag = featureFlag;
    }

    public void NotFound()
    {
        FeatureFlag = new FeatureFlagNull();
        IsNotFound = true;
    }
}