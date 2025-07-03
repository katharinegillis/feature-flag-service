using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.Get;

public interface ICodePresenter : IPresenter
{
    public IModel FeatureFlag { get; }
    public bool IsNotFound { get; }
}