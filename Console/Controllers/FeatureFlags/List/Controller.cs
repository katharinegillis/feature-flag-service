using Application.Interactors.ListFeatureFlags;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

public sealed class Controller(IConsolePresenterFactory factory, IInputPort interactor) : IExecutable
{
    public async Task<int> Execute()
    {
        var presenter = factory.Create();

        await interactor.Execute(presenter);

        return presenter.ExitCode;
    }
}