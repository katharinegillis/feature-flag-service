namespace Domain.Common;

public interface IValidatable
{
    public Result<bool, IEnumerable<ValidationError>> Validate();
}