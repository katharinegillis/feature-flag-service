using Application.Interactors.DeleteFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Delete;

public interface IConsolePresenter : IHasExitCode, IOutputPort
{
    public RequestModel Request { get; }
}