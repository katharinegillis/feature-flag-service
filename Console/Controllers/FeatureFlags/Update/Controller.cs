using Application.UseCases.FeatureFlag.Update;
using Console.Common;

namespace Console.Controllers.FeatureFlags.Update;

public sealed class Controller(IConsolePresenterFactory factory, IUseCase interactor) : IExecutable, IHasOptions
{
    private IOptions _options = null!;

    public async Task<IConsoleActionResult> Execute()
    {
        var request = new RequestModel
        {
            Id = _options.Id,
            Enabled = (bool)_options.Enabled!
        };

        var presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        return presenter.ActionResult;
    }

    public void SetOptions(object options)
    {
        _options = (IOptions)options;
    }
}