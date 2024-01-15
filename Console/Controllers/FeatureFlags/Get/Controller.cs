using Application.Interactors.GetFeatureFlag;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Get;

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
            Id = _options.Id
        };

        var presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        return presenter.ExitCode;
    }
}