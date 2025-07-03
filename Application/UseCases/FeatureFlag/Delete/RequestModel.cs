namespace Application.UseCases.FeatureFlag.Delete;

public record RequestModel
{
    public required string Id { get; init; }
}