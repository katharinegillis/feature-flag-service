namespace Domain.Models;

public class FeatureFlag
{
    public required string Id { get; set; }
    
    public required bool Enabled { get; set; }
}