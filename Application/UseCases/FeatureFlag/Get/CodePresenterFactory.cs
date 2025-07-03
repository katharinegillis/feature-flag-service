namespace Application.UseCases.FeatureFlag.Get;

public sealed class CodePresenterFactory : ICodePresenterFactory
{
    public ICodePresenter Create()
    {
        return new CodePresenter();
    }
}