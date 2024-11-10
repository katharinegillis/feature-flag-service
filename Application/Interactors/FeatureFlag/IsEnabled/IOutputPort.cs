namespace Application.Interactors.FeatureFlag.IsEnabled;

public interface IOutputPort
{
    public void Ok(bool enabled);

    public void NotFound();
}