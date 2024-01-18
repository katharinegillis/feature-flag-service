namespace Application.Interactors.CreateFeatureFlag;

public class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}