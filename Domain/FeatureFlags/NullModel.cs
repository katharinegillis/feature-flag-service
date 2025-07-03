using Domain.Common;

namespace Domain.FeatureFlags;

public sealed class NullModel : IModel
{
    private readonly bool _enabled;

    private readonly string _id = "";
    public static NullModel Instance { get; } = new();

    public bool IsNull => true;

    public string Id
    {
        get => _id;
        // ReSharper disable once ValueParameterNotUsed
        init => _id = "";
    }

    public bool Enabled
    {
        get => _enabled;
        // ReSharper disable once ValueParameterNotUsed
        init => _enabled = false;
    }

    public Result<bool, IEnumerable<ValidationError>> Validate()
    {
        return new List<ValidationError>
        {
            new()
            {
                Field = "Id",
                Message = "Null object"
            }
        };
    }
}