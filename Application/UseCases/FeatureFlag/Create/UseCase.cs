using Domain.Common;
using Domain.FeatureFlags;

namespace Application.UseCases.FeatureFlag.Create;

public sealed class UseCase(IRepository repository, IFactory factory) : IUseCase
{
    public async Task Execute(RequestModel request, IPresenter presenter)
    {
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
            presenter.Ok();
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