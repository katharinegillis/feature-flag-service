using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Models;

public class FeatureFlag
{
    [MaxLength(100)] public required string FeatureFlagId { get; set; }
    public bool Enabled { get; set; }
}