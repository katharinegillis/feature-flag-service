using Application.Interactors.Config.Show;
using Console.Common;
using Domain.Common;

namespace Console.Controllers.Config.Show;

public interface IConsolePresenter : IOutputPort, IHasExitCode
{
    public RequestModel? Request { get; }

    public void BadRequest(IEnumerable<ValidationError> validationErrors);
}