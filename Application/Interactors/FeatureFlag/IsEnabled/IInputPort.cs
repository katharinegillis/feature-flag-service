namespace Application.Interactors.FeatureFlag.IsEnabled;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}