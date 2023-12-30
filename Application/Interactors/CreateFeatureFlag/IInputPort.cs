namespace Application.Interactors.CreateFeatureFlag;

public interface IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter);
}