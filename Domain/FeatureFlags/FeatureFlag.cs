namespace Domain.FeatureFlags;

public sealed class FeatureFlag : IFeatureFlag
{
    public bool IsNull => false;

    public required string Id { get; init; }

    public required bool Enabled { get; set; }
}