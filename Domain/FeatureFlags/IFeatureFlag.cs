using Domain.Common;

namespace Domain.FeatureFlags;

public interface IFeatureFlag : INullable
{
    public string Id { get; }

    public bool Enabled { get; }
}