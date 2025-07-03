namespace Application.UseCases.FeatureFlag.IsEnabled;

public interface IPresenter
{
    public void Ok(bool enabled);

    public void NotFound();
}