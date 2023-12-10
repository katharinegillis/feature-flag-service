using IGetFeatureFlagInputPort = Application.Interactors.GetFeatureFlag.IInputPort;
using GetFeatureFlagCodePresenter = Application.Interactors.GetFeatureFlag.CodePresenter;
using GetFeatureFlagRequestModel = Application.Interactors.GetFeatureFlag.RequestModel;

namespace Application.Interactors.IsFeatureFlagEnabled;

public class Interactor(IGetFeatureFlagInputPort getFeatureFlagInteractor) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        // Get the feature flag.
        var getFeatureFlagCodePresenter = new GetFeatureFlagCodePresenter();
        await getFeatureFlagInteractor.Execute(new GetFeatureFlagRequestModel { Id = request.Id },
            getFeatureFlagCodePresenter);
        var featureFlag = getFeatureFlagCodePresenter.FeatureFlag;

        if (featureFlag is not null)
        {
            // Send the enabled status.
            presenter.Ok(featureFlag.Enabled);
            return;
        }

        presenter.NotFound();
    }
}