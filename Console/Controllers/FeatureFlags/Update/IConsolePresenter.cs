using Application.Interactors.FeatureFlag.Update;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
    public RequestModel Request { get; }
}