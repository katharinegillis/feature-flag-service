using Application.Interactors.Config.Show;
using Console.Common;

namespace Console.Controllers.Config.Show;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
    public RequestModel Request { get; }
}