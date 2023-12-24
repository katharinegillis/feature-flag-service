using Application.Interactors.CreateFeatureFlag;
using Console.Common;

namespace Console.Commands.FeatureFlags.Create;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
}