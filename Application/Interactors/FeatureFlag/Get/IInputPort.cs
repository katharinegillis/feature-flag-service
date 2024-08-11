namespace Application.Interactors.FeatureFlag.Get;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}