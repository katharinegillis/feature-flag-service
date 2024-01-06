using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public interface ICodePresenter : IOutputPort
{
    public IEnumerable<IModel> FeatureFlags { get; }
}