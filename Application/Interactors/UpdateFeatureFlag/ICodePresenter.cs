using Domain.Common;

namespace Application.Interactors.UpdateFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    // TODO: create codepresenterfactories and convert isfeatureflagenabled to use the factory
    public bool IsOk { get; }

    public IEnumerable<Error> Errors { get; }

    public bool IsNotFound { get; }
}