namespace Application.UseCases.FeatureFlag.IsEnabled;

public sealed record RequestModel
{
    public required string Id { get; init; }
}