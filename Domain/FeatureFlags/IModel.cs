using Domain.Common;

namespace Domain.FeatureFlags;

public interface IModel : INullable, IValidatable
{
    public string Id { get; }

    public bool Enabled { get; }
}