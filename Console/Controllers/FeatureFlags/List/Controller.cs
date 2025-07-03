using Application.UseCases.FeatureFlag.List;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

public sealed class Controller(IConsolePresenterFactory factory, IUseCase interactor) : IExecutable
{
    public async Task<IConsoleActionResult> Execute()
    {
        var presenter = factory.Create();

        await interactor.Execute(presenter);

        return presenter.ActionResult;
    }
}