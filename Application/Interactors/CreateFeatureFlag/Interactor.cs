using Domain.Common;
using Domain.FeatureFlags;

namespace Application.Interactors.CreateFeatureFlag;

public sealed class Interactor(IRepository repository, IFactory factory) : IInputPort
{
    public async Task Execute(RequestModel request, IOutputPort presenter)
    {
        presenter.Request = request;

        var featureFlag = factory.Create(request.Id, request.Enabled);
        var validationResult = featureFlag.Validate();
        if (!validationResult.IsOk)
        {
            presenter.BadRequest(validationResult.Error);
            return;
        }

        var result = await repository.Create(featureFlag);
        if (result.IsOk)
        {
            presenter.Ok(result.Value);
            return;
        }

        if (result.Error is ValidationError validationError)
        {
            presenter.BadRequest(new List<ValidationError> { validationError });
            return;
        }

        presenter.Error(result.Error);
    }
}