namespace Application.Interactors.Config.Show;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}