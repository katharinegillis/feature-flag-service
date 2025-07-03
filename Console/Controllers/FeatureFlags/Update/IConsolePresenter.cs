using Application.UseCases.FeatureFlag.Update;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

public interface IConsolePresenter : IPresenter, IHasActionResult
{
    public RequestModel Request { get; }
}