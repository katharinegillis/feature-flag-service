namespace Application.Interactors.ListFeatureFlags;

public interface IInputPort
{
    public Task Execute(IOutputPort presenter);
}