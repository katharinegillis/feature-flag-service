using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public sealed class CodePresenter : IOutputPort, ICodePresenter
{
    public IFeatureFlag FeatureFlag { get; private set; } = new FeatureFlagNull();
    public bool IsNotFound { get; private set; }

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