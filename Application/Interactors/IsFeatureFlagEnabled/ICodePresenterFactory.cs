namespace Application.Interactors.IsFeatureFlagEnabled;

public interface ICodePresenterFactory
{
    public ICodePresenter Create();
}