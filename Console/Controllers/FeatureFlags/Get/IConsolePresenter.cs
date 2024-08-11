using Application.Interactors.FeatureFlag.Get;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Get;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
    public RequestModel Request { get; }
}