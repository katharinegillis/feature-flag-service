namespace Application.Interactors.FeatureFlag.Update;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}