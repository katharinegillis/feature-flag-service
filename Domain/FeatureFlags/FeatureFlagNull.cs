using Domain.Common;

namespace Domain.FeatureFlags;

public sealed class FeatureFlagNull : IFeatureFlag
{
    public static FeatureFlagNull Instance { get; } = new();

    public bool IsNull => true;

    private readonly string _id = "";

    public string Id
    {
        get => _id;
        // ReSharper disable once ValueParameterNotUsed
        init => _id = "";
    }

    private bool _enabled;

    public bool Enabled
    {
        get => _enabled;
        // ReSharper disable once ValueParameterNotUsed
        set => _enabled = false;
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