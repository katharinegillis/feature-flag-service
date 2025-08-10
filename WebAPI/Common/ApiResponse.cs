namespace WebAPI.Common;

public sealed class ApiResponse<T>
{
    public required bool Successful { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}