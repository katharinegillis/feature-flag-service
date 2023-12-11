using Domain.FeatureFlag;

namespace Application.Interactors.GetFeatureFlag;

public class Interactor(IFeatureFlagRepository featureFlagRepository) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        // Get the feature flag from the repository.
        var featureFlag = await featureFlagRepository.Get(request.Id);

        if (featureFlag.IsNull)
        {
            presenter.NotFound();
            return;
        }

        presenter.Ok(featureFlag);
    }
}