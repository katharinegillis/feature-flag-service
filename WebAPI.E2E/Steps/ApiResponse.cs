namespace WebAPI.E2E.Steps;

// ReSharper disable once ClassNeverInstantiated.Global
public sealed class ApiResponse<T>
{
    public required bool Successful { get; set; }
    public T? Data { get; set; }
    public List<string>? Errors { get; set; }
}