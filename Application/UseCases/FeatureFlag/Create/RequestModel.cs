namespace Application.UseCases.FeatureFlag.Create;

public sealed record RequestModel
{
    public required string Id { get; init; }

    public required bool Enabled { get; init; }
}