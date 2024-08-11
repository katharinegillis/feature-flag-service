namespace Application.Interactors.FeatureFlag.IsEnabled;

public sealed record RequestModel
{
    public required string Id { get; init; }
}