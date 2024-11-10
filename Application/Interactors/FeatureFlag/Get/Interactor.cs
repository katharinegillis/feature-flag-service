using Domain.FeatureFlags;

namespace Application.Interactors.FeatureFlag.Get;

public sealed class Interactor(IReadRepository repository) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        var featureFlag = await repository.Get(request.Id);

        if (featureFlag.IsNull)
        {
            presenter.NotFound();
            return;
        }

        presenter.Ok(featureFlag);
    }
}