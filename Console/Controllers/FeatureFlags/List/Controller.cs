using Application.Interactors.ListFeatureFlags;
using Console.Common;

namespace Console.Controllers.FeatureFlags.List;

public class Controller(IConsolePresenter presenter, IInputPort interactor) : IRunnableWithOptions
{
    public void SetOptions(object options)
    {
    }

    public async Task<int> Run()
    {
        await interactor.Execute(presenter);

        return presenter.ExitCode;
    }
}