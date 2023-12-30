namespace Application.Interactors.IsFeatureFlagEnabled;

public sealed record RequestModel
{
    public required string Id { get; init; }
}