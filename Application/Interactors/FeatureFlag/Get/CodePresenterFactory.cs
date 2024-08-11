namespace Application.Interactors.FeatureFlag.Get;

public sealed class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}