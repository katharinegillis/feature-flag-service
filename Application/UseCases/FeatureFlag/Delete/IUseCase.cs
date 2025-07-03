namespace Application.UseCases.FeatureFlag.Delete;

public interface IUseCase
{
    public Task Execute(RequestModel request, IPresenter presenter);
}