namespace Domain.FeatureFlag;

public class FeatureFlagNull : IFeatureFlag
{
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