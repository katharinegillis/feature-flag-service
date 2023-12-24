namespace Console.Commands.FeatureFlags.Create;

public interface IOptions
{
    public string Id { get; }

    public bool Enabled { get; }
}