using Application.Interactors.Config.Show;
using Console.Common;

namespace Console.Controllers.Config.Show;

public sealed class Controller(IConsolePresenterFactory factory, IInputPort interactor) : IExecutable, IHasOptions
{
    private IOptions _options = null!;

    public void SetOptions(object options)
    {
        _options = (IOptions)options;
    }

    public Task<int> Execute()
    {
        // TODO How to handle the string options to enum request
    }
}