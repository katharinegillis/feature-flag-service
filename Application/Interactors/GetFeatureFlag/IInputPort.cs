namespace Application.Interactors.GetFeatureFlag;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}