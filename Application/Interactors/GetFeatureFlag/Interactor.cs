using Domain.FeatureFlags;

namespace Application.Interactors.GetFeatureFlag;

public sealed class Interactor(IRepository repository) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        // Get the feature flag from the repository.
        var featureFlag = await repository.Get(request.Id);

        if (featureFlag.IsNull)
        {
            presenter.NotFound();
            return;
        }

        presenter.Ok(featureFlag);
    }
}