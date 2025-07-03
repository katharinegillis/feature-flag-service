namespace Application.UseCases.FeatureFlag.IsEnabled;

public interface IUseCase
{
    public Task Execute(RequestModel request, IPresenter presenter);
}