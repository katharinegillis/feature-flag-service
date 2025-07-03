namespace Domain.FeatureFlags;

public sealed class EqualityComparer : IEqualityComparer<IModel>
{
    public bool Equals(IModel? x, IModel? y)
    {
        if (ReferenceEquals(x, y)) return true;

        if (x is null || y is null) return false;

        return x.Id == y.Id && x.Enabled == y.Enabled;
    }

    public int GetHashCode(IModel obj)
    {
        return (obj.Id, obj.Enabled).GetHashCode();
    }
}