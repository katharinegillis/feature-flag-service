namespace Application.Interactors.IsFeatureFlagEnabled;

public sealed class CodePresenter : ICodePresenter
{
    public bool Enabled { get; private set; }
    public bool IsNotFound { get; private set; }

    public void Ok(bool enabled)
    {
        Enabled = enabled;
    }

    public void NotFound()
    {
        IsNotFound = true;
    }
}