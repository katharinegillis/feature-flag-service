using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.Get;

public sealed class UseCase(IReadRepository repository) : IUseCase
{
    public async Task Execute(RequestModel request, IPresenter presenter)
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