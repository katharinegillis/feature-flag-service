using System.Diagnostics.CodeAnalysis;

namespace Domain.Common;

public sealed record NotFoundError : Error
{
    [SetsRequiredMembers]
    public NotFoundError()
    {
        Message = "Not found";
    }
}