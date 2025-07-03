using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.List;

public sealed class UseCase(IReadRepository repository) : IUseCase
{
    public async Task Execute(IPresenter presenter)
    {
        var featureFlags = await repository.List();

        presenter.Ok(featureFlags);
    }
}