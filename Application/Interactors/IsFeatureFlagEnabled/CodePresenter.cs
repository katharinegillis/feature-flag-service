namespace Application.Interactors.IsFeatureFlagEnabled;

public sealed class CodePresenter : IOutputPort
{
    public bool Enabled;
    public bool IsNotFound;

    public void Ok(bool enabled)
    {
        Enabled = enabled;
    }

    public void NotFound()
    {
        IsNotFound = true;
    }
}