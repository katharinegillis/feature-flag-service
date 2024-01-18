using Domain.Common;

namespace Application.Interactors.UpdateFeatureFlag;

public sealed class CodePresenter : ICodePresenter
{
    public void Ok()
    {
        IsOk = true;
    }

    public void BadRequest(IEnumerable<ValidationError> validationErrors)
    {
        IsOk = false;
        Errors = validationErrors;
    }

    public void Error(Error error)
    {
        IsOk = false;
        Errors = new List<Error>
        {
            error
        };
    }

    public void NotFound()
    {
        IsOk = false;
        IsNotFound = true;
    }

    public bool IsOk { get; private set; }

    public IEnumerable<Error> Errors { get; private set; } = null!;

    public bool IsNotFound { get; private set; }
}