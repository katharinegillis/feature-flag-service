namespace Application.Interactors.IsFeatureFlagEnabled;

public class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}