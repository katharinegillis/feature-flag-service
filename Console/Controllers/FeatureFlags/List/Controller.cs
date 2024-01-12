using Application.Interactors.ListFeatureFlags;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

public sealed class Controller(IConsolePresenter presenter, IInputPort interactor) : IExecutable
{
    public async Task<int> Execute()
    {
        await interactor.Execute(presenter);

        return presenter.ExitCode;
    }
}