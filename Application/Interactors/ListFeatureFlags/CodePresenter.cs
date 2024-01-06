using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public sealed class CodePresenter : ICodePresenter
{
    public IEnumerable<IModel> FeatureFlags { get; private set; } = null!;

    public void Ok(IEnumerable<IModel> featureFlags)
    {
        FeatureFlags = featureFlags;
    }
}