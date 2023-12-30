using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    public IModel FeatureFlag { get; }
    public bool IsNotFound { get; }
}