using Domain.Common;

namespace Application.Interactors.UpdateFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    public bool IsOk { get; }

    public IEnumerable<Error> Errors { get; }

    public bool IsNotFound { get; }
}