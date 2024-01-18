using IGetFeatureFlagInputPort = Application.Interactors.GetFeatureFlag.IInputPort;
using IGetFeatureFlagCodePresenter = Application.Interactors.GetFeatureFlag.ICodePresenter;
using IGetFeatureFlagCodePresenterFactory = Application.Interactors.GetFeatureFlag.ICodePresenterFactory;
using GetFeatureFlagRequestModel = Application.Interactors.GetFeatureFlag.RequestModel;

namespace Application.Interactors.IsFeatureFlagEnabled;

public sealed class Interactor(
    IGetFeatureFlagCodePresenterFactory getFeatureFlagCodePresenterFactory,
    IGetFeatureFlagInputPort getFeatureFlagInteractor) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        var getFeatureFlagCodePresenter = getFeatureFlagCodePresenterFactory.Create();

        // Get the feature flag.
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