using Domain.Common;

namespace Application.UseCases.FeatureFlag.Update;

public interface IPresenter
{
    public void Ok();

    public void BadRequest(IEnumerable<ValidationError> validationErrors);

    public void Error(Error error);

    public void NotFound();
}