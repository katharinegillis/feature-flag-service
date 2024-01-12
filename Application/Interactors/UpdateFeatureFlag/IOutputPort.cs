using Domain.Common;

namespace Application.Interactors.UpdateFeatureFlag;

public interface IOutputPort
{
    public void Ok();

    public void BadRequest(IEnumerable<ValidationError> validationErrors);

    public void Error(Error error);

    public void NotFound();

    public RequestModel? Request { get; set; }
}