namespace Domain.FeatureFlag;

public class FeatureFlag : IFeatureFlag
{
    public bool IsNull => false;

    public required string Id { get; set; }

    public required bool Enabled { get; set; }
}