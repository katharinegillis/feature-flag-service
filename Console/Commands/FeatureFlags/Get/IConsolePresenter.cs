using Application.Interactors.GetFeatureFlag;
using Console.Common;

namespace Console.Commands.FeatureFlags.Get;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
}