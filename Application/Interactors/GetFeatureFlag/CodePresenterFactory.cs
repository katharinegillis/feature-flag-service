namespace Application.Interactors.GetFeatureFlag;

public class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}