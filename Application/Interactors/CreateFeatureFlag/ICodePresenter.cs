using Domain.Common;

namespace Application.Interactors.CreateFeatureFlag;

public interface ICodePresenter : IOutputPort
{
    // ReSharper disable once UnusedMemberInSuper.Global
    public string Id { get; }

    // ReSharper disable once UnusedMemberInSuper.Global
    public IEnumerable<Error> Errors { get; }

    // ReSharper disable once UnusedMemberInSuper.Global
    public bool IsOk { get; }
}