namespace WebAPI.Common;

public interface IHasIsError
{
    public bool IsError { get; }
    public string? Message { get; }
}