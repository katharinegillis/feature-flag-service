using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    public IFeatureFlag FeatureFlag { get; }
    public bool IsNotFound { get; }
}