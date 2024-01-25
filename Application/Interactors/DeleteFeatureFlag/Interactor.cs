using Domain.Common;
using Domain.FeatureFlags;

namespace Application.Interactors.DeleteFeatureFlag;

public sealed class Interactor(IRepository repository) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
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