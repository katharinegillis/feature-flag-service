namespace Application.UseCases.FeatureFlag.Create;

public interface IUseCase
{
    public Task Execute(RequestModel request, IPresenter presenter);
}