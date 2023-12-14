using Domain.Common;

namespace Domain.FeatureFlags;

public interface IFeatureFlag : INullable
{
    public string Id { get; init; }

    public bool Enabled { get; set; }
}