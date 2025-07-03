using Application.UseCases.Config.Show;
using Console.Common;
using Domain.Common;

namespace Console.Controllers.Config.Show;

public interface IConsolePresenter : IPresenter, IHasActionResult
{
    public RequestModel? Request { get; }

    public void BadRequest(IEnumerable<ValidationError> validationErrors);
}