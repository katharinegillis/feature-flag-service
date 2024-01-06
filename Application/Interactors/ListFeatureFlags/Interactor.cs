using Domain.FeatureFlags;

namespace Application.Interactors.ListFeatureFlags;

// TODO: make everything sealed/record where possible!

public sealed class Interactor(IRepository repository) : IInputPort
{
    public async Task Execute(IOutputPort presenter)
    {
        var featureFlags = await repository.List();

        presenter.Ok(featureFlags);
    }
}