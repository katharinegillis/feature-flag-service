namespace Application.UseCases.FeatureFlag.Update;

public sealed record RequestModel
{
    public required string Id { get; init; }

    public required bool Enabled { get; init; }
}