namespace Application.Interactors.Config.Show;

public sealed class RequestModel
{
    public enum NameOptions
    {
        Datasource
    }

    public NameOptions Name { get; init; }
}