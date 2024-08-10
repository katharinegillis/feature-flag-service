namespace WebAPI.E2E.Models;

public record FeatureFlag
{
    public string Id { get; set; } = null!;
    public bool Enabled { get; set; }
}