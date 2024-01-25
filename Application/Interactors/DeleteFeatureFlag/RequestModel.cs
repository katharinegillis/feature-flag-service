namespace Application.Interactors.DeleteFeatureFlag;

public record RequestModel
{
    public required string Id { get; init; }
}