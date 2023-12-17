namespace Application.Interactors.IsFeatureFlagEnabled;

public interface ICodePresenter : IOutputPort
{
    public bool Enabled { get; }
    public bool IsNotFound { get; }
}