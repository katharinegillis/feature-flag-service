namespace Application.Interactors.FeatureFlag.List;

public interface IInputPort
{
    public Task Execute(IOutputPort presenter);
}