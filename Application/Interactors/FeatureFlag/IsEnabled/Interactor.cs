using IGetFeatureFlagInputPort = Application.Interactors.FeatureFlag.Get.IInputPort;
using IGetFeatureFlagCodePresenterFactory = Application.Interactors.FeatureFlag.Get.ICodePresenterFactory;
using GetFeatureFlagRequestModel = Application.Interactors.FeatureFlag.Get.RequestModel;

namespace Application.Interactors.FeatureFlag.IsEnabled;

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