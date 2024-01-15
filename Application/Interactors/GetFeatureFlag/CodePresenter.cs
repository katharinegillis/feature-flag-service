using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public sealed class CodePresenter : ICodePresenter
{
    public IModel FeatureFlag { get; private set; } = new NullModel();
    public bool IsNotFound { get; private set; }

    public void Ok(IModel featureFlag)
    {
        FeatureFlag = featureFlag;
    }

    public void NotFound()
    {
        FeatureFlag = new NullModel();
        IsNotFound = true;
    }
}