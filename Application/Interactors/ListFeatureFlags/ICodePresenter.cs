using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public interface ICodePresenter : IOutputPort
{
    // ReSharper disable once UnusedMemberInSuper.Global
    public IEnumerable<IModel> FeatureFlags { get; }
}