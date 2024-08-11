using Domain.FeatureFlags;

namespace Application.Interactors.FeatureFlag.Get;

public interface ICodePresenter : IOutputPort
{
    public IModel FeatureFlag { get; }
    public bool IsNotFound { get; }
}