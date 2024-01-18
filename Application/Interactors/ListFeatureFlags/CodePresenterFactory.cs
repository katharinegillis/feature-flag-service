namespace Application.Interactors.ListFeatureFlags;

public class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}