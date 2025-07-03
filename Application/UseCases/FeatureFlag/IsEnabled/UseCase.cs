using IGetFeatureFlagCodePresenterFactory = Application.UseCases.FeatureFlag.Get.ICodePresenterFactory;
using GetFeatureFlagRequestModel = Application.UseCases.FeatureFlag.Get.RequestModel;

namespace Application.UseCases.FeatureFlag.IsEnabled;

public sealed class UseCase(
    IGetFeatureFlagCodePresenterFactory getFeatureFlagCodePresenterFactory,
    Get.IUseCase getFeatureFlagInteractor) : IUseCase
{
    public async Task Execute(RequestModel request, IPresenter presenter)
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