using Domain.Common;

namespace Application.UseCases.FeatureFlag.Delete;

public interface IPresenter
{
    public void Ok();

    public void NotFound();

    public void Error(Error error);
}