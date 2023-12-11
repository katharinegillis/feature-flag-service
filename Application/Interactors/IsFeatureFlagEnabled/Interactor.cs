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

        if (getFeatureFlagCodePresenter.IsNotFound)
        {
            // Not found
            presenter.NotFound();
            return;
        }

        presenter.Ok(getFeatureFlagCodePresenter.FeatureFlag.Enabled);
    }
}