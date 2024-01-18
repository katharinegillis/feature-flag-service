namespace Application.Interactors.UpdateFeatureFlag;

public class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}