using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public sealed class Interactor(IRepository repository) : IInputPort
{
    public async Task Execute(IOutputPort presenter)
    {
        var featureFlags = await repository.List();

        presenter.Ok(featureFlags);
    }
}