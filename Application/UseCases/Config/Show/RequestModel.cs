namespace Application.UseCases.Config.Show;

public sealed class RequestModel : IEquatable<RequestModel>
{
    public enum NameOptions
    {
        Unknown,
        Datasource
    }

    public NameOptions Name { get; init; }

    public bool Equals(RequestModel? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        return ReferenceEquals(this, obj) || (obj is RequestModel other && Equals(other));
    }

    public override int GetHashCode()
    {
        return (int)Name;
    }
}