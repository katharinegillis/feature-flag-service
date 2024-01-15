namespace Application.Interactors.IsFeatureFlagEnabled;

public interface IOutputPort
{
    public void Ok(bool enabled);

    public void NotFound();
}