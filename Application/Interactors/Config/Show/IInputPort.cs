namespace Application.Interactors.Config.Show;

public interface IInputPort
{
    public void Execute(RequestModel request, IOutputPort presenter);
}