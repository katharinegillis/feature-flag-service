namespace Application.UseCases.FeatureFlag.Update;

public interface IUseCase
{
    public Task Execute(RequestModel request, IPresenter presenter);
}