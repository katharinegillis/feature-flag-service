namespace Application.Interactors.FeatureFlag.Create;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}