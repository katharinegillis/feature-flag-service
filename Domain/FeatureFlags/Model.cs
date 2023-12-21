using Domain.Common;

namespace Domain.FeatureFlags;

public sealed class Model : IModel
{
    public bool IsNull => false;

    public required string Id { get; init; }

    public required bool Enabled { get; set; }

    public Result<bool, IEnumerable<ValidationError>> Validate()
    {
        var errors = new List<ValidationError>();

        if (Id.Length > 100)
        {
            errors.Add(new ValidationError
            {
                Field = "Id",
                Message = "Max length is 100"
            });
        }

        if (errors.Count != 0)
        {
            return errors;
        }

        return true;
    }
}