namespace Application.Interactors.IsFeatureFlagEnabled;

public interface ICodePresenter : IOutputPort
{
    // ReSharper disable once UnusedMemberInSuper.Global
    public bool Enabled { get; }

    // ReSharper disable once UnusedMemberInSuper.Global
    public bool IsNotFound { get; }
}