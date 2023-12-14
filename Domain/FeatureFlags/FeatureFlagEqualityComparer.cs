namespace Domain.FeatureFlags;

public class FeatureFlagEqualityComparer : IEqualityComparer<IFeatureFlag>
{
    public bool Equals(IFeatureFlag? x, IFeatureFlag? y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (x is null || y is null)
        {
            return false;
        }

        return x.Id == y.Id && x.Enabled == y.Enabled;
    }

    public int GetHashCode(IFeatureFlag obj)
    {
        return (obj.Id, obj.Enabled).GetHashCode();
    }
}