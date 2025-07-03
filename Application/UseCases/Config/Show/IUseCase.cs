namespace Application.UseCases.Config.Show;

public interface IUseCase
{
    public void Execute(RequestModel request, IPresenter presenter);
}