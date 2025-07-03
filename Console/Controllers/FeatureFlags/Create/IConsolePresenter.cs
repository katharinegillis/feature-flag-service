using Application.UseCases.FeatureFlag.Create;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Create;

public interface IConsolePresenter : IPresenter, IHasActionResult
{
    public RequestModel Request { get; }
}