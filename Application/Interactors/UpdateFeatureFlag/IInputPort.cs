namespace Application.Interactors.UpdateFeatureFlag;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}