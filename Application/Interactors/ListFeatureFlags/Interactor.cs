using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

public sealed class Interactor(IReadRepository repository) : IInputPort
{
    public async Task Execute(IOutputPort presenter)
    {
        var featureFlags = await repository.List();

        presenter.Ok(featureFlags);
    }
}