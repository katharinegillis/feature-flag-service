namespace Domain.Common;

public readonly struct Result<T, TE>
{
    public readonly T Value;
    public readonly TE Error;

    private Result(T v, TE e, bool success)
    {
        Value = v;
        Error = e;
        IsOk = success;
    }

    public bool IsOk { get; }

    public static Result<T, TE> Ok(T v)
    {
        return new Result<T, TE>(v, default!, true);
    }

    public static Result<T, TE> Err(TE e)
    {
        return new Result<T, TE>(default!, e, false);
    }

    public static implicit operator Result<T, TE>(T v)
    {
        return new Result<T, TE>(v, default!, true);
    }

    public static implicit operator Result<T, TE>(TE e)
    {
        return new Result<T, TE>(default!, e, false);
    }
}