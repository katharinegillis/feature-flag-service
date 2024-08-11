namespace Application.Interactors.FeatureFlag.Delete;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}