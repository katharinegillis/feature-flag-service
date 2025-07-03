using Domain.Common;
using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.Delete;

public sealed class UseCase(IRepository repository) : IUseCase
{
    public async Task Execute(RequestModel request, IPresenter presenter)
    {
        var result = await repository.Delete(request.Id);
        if (result.IsOk)
        {
            presenter.Ok();
            return;
        }

        if (result.Error is NotFoundError)
        {
            presenter.NotFound();
            return;
        }

        presenter.Error(result.Error);
    }
}