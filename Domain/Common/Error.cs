namespace Domain.Common;

public record Error
{
    public required string Message { get; init; }
}