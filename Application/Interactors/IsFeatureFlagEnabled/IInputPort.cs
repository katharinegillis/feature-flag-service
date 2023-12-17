namespace Application.Interactors.IsFeatureFlagEnabled;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}