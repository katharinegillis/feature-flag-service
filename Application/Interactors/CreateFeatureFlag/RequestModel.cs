namespace Application.Interactors.CreateFeatureFlag;

public record RequestModel
{
    public required string Id { get; init; }

    public required bool Enabled { get; init; }
}