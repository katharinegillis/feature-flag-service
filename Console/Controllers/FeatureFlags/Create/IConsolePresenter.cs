using Application.Interactors.CreateFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Create;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
    public RequestModel Request { get; }
}