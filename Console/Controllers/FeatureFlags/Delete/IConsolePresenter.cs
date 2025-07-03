using Application.UseCases.FeatureFlag.Delete;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Delete;

public interface IConsolePresenter : IHasActionResult, IPresenter
{
    public RequestModel Request { get; }
}