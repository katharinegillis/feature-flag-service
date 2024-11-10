using Domain.FeatureFlags;

namespace Application.Interactors.Config.Show;

public sealed class Interactor(IReadRepository repository) : IInputPort
{
    public Task Execute(RequestModel request, IOutputPort presenter)
    {
        switch (request.Name)
        {
            case RequestModel.NameOptions.Datasource:
                presenter.Ok(repository.Name);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(request));
        }

        return Task.CompletedTask;
    }
}