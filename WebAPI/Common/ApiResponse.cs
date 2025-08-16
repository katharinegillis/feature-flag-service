namespace WebAPI.Common;

public sealed class ApiResponse<T>
{
    public required bool Successful { get; init; }
    public T? Data { get; init; }
    public List<string>? Errors { get; init; }
}