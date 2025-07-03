namespace Application.UseCases.FeatureFlag.List;

public interface IUseCase
{
    public Task Execute(IPresenter presenter);
}