namespace Application.Interactors.GetFeatureFlag;

public sealed class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}