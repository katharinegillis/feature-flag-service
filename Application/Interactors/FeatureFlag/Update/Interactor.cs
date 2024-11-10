using Domain.Common;
using Domain.FeatureFlags;

namespace Application.Interactors.FeatureFlag.Update;

public sealed class Interactor(IRepository repository, IFactory factory) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        var featureFlag = factory.Create(request.Id, request.Enabled);
        var validationResult = featureFlag.Validate();
        if (!validationResult.IsOk)
        {
            presenter.BadRequest(validationResult.Error);
            return;
        }

        var result = await repository.Update(featureFlag);
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