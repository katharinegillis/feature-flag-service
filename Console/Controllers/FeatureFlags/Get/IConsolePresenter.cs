using Application.UseCases.FeatureFlag.Get;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Get;

public interface IConsolePresenter : IPresenter, IHasActionResult
{
    public RequestModel Request { get; }
}