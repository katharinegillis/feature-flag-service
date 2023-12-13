namespace Domain.FeatureFlag;

public sealed class FeatureFlagNull : IFeatureFlag
{
    public static FeatureFlagNull Instance { get; } = new FeatureFlagNull();

    public bool IsNull => true;

    private string _id = "";

    public string Id
    {
        get => _id;
        // ReSharper disable once ValueParameterNotUsed
        set => _id = "";
    }

    private bool _enabled;

    public bool Enabled
    {
        get => _enabled;
        // ReSharper disable once ValueParameterNotUsed
        set => _enabled = false;
    }
}