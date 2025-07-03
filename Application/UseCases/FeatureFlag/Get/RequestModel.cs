namespace Application.UseCases.FeatureFlag.Get;

public sealed record RequestModel
{
    public required string Id { get; init; }
}