using Domain.Common;

namespace Domain.FeatureFlag;

public interface IFeatureFlag : INullable
{
    public string Id { get; set; }

    public bool Enabled { get; set; }
}