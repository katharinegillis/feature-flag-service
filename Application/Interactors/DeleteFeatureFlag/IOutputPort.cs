using Domain.Common;

namespace Application.Interactors.DeleteFeatureFlag;

public interface IOutputPort
{
    public void Ok();

    public void NotFound();

    public void Error(Error error);
}