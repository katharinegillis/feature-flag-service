namespace Application.Interactors.DeleteFeatureFlag;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}