using Domain.Common;

namespace Application.Interactors.CreateFeatureFlag;

public interface IOutputPort
{
    public void Ok(string id);

    public void BadRequest(IEnumerable<ValidationError> validationErrors);

    public void Error(Error error);
}