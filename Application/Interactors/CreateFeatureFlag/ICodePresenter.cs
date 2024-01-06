using Domain.Common;

namespace Application.Interactors.CreateFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    public string Id { get; }

    public IEnumerable<Error> Errors { get; }

    public bool IsOk { get; }
}