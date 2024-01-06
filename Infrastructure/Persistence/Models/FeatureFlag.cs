using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Persistence.Models;

public sealed record FeatureFlag
{
    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    [MaxLength(100)] public required string FeatureFlagId { get; set; }

    // ReSharper disable once PropertyCanBeMadeInitOnly.Global
    public bool Enabled { get; set; }
}