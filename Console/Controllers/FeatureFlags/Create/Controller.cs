using Application.Interactors.CreateFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Create;

public sealed class Controller(IConsolePresenter presenter, IInputPort interactor) : IRunnableWithOptions
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
            Id = _options.Id,
            Enabled = _options.Enabled
        };

        await interactor.Execute(request, presenter);

        return presenter.ExitCode;
    }
}