using Domain.Common;

namespace Application.Interactors.FeatureFlag.Delete;

public interface IOutputPort
{
    public void Ok();

    public void NotFound();

    public void Error(Error error);
}