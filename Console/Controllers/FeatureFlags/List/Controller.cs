using Application.Interactors.FeatureFlag.List;
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