namespace Application.UseCases.FeatureFlag.Get;

public interface IUseCase
{
    public Task Execute(RequestModel request, IPresenter presenter);
}