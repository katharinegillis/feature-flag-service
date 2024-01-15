using Application.Interactors.UpdateFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

public sealed class Controller(IConsolePresenterFactory factory, IInputPort interactor) : IExecutable, IHasOptions
{
    private IOptions _options = null!;

    public void SetOptions(object options)
    {
        _options = (IOptions)options;
    }

    public async Task<int> Execute()
    {
        var request = new RequestModel
        {
            Id = _options.Id,
            Enabled = (bool)_options.Enabled!
        };

        var presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        return presenter.ExitCode;
    }
}