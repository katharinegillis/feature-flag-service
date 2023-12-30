namespace Application.Interactors.GetFeatureFlag;

public sealed record RequestModel
{
    public required string Id { get; init; }
}