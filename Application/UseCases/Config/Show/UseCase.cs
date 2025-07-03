using Domain.FeatureFlags;

namespace Application.UseCases.Config.Show;

public sealed class UseCase(IReadRepository repository) : IUseCase
{
    public void Execute(RequestModel request, IPresenter presenter)
    {
        switch (request.Name)
        {
            case RequestModel.NameOptions.Datasource:
                presenter.Ok(repository.Name);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request));
        }
    }
}