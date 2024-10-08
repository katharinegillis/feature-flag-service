using Application.Interactors.Config.Show;
using Console.Common;
using Domain.Common;

namespace Console.Controllers.Config.Show;

public sealed class Controller(IConsolePresenterFactory factory, IInputPort interactor) : IExecutable, IHasOptions
{
    private IOptions _options = null!;

    public void SetOptions(object options)
    {
        _options = (IOptions)options;
    }

    public async Task<int> Execute()
    {
        RequestModel request;

        IConsolePresenter presenter;
        
        switch (_options.Name.ToLower())
        {
            case "datasource":
                request = new RequestModel
                {
                    Name = RequestModel.NameOptions.Datasource
                };
                break;
            default:
                var errors = new List<ValidationError>
                {
                    new()
                    {
                        Field = "First argument",
                        Message = "Must be one of: datasource"
                    }
                };
                presenter = factory.Create(null);
                presenter.BadRequest(errors);
                return presenter.ExitCode;
        }

        presenter = factory.Create(request);

        await interactor.Execute(request, presenter);

        return presenter.ExitCode;
    }
}