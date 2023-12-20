namespace Domain.Common;

public record ValidationError : Error
{
    public required string Field { get; init; }
}