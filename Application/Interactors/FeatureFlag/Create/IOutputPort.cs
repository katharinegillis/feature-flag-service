using Domain.Common;

namespace Application.Interactors.FeatureFlag.Create;

public interface IOutputPort
{
    public void Ok();

    public void BadRequest(IEnumerable<ValidationError> validationErrors);

    public void Error(Error error);
}