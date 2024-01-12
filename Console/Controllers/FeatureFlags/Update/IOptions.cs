namespace Console.Controllers.FeatureFlags.Update;

public interface IOptions
{
    public string Id { get; }

    public bool? Enabled { get; }
}