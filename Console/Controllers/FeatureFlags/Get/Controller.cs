using Application.Interactors.GetFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Get;

public class Controller(IConsolePresenter presenter, IInputPort interactor) : IRunnableWithOptions
{
    private IOptions _options = null!;

    public void SetOptions(object options)
    {
        _options = (IOptions)options;
    }

    public async Task<int> Run()
    {
        var request = new RequestModel
        {
            Id = _options.Id
        };

        await interactor.Execute(request, presenter);

        return presenter.ExitCode;
    }
}