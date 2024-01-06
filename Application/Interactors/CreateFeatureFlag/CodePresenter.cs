using Domain.Common;

namespace Application.Interactors.CreateFeatureFlag;

public sealed class CodePresenter : ICodePresenter
{
    public void Ok(string id)
    {
        Id = id;
        IsOk = true;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        Errors = validationErrors;
        IsOk = false;
    }

    public void Error(Error error)
    {
        Errors = new List<Error>
        {
            error
        };
        IsOk = false;
    }

    public string Id { get; private set; } = null!;

    public IEnumerable<Error> Errors { get; private set; } = null!;

    public bool IsOk { get; private set; }
}