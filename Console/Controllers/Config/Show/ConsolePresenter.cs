using Application.Interactors.Config.Show;
using Domain.Common;

namespace Console.Controllers.Config.Show;

public class ConsolePresenter(RequestModel? request) : IConsolePresenter
{
    public void Ok(string value)
    {
        ExitCode = (int)Common.ExitCode.Success;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        ExitCode = (int)Common.ExitCode.OptionsError;
    }

    public int ExitCode { get; private set; }
    public RequestModel? Request => request;
}