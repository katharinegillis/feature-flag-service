using WebAPI.E2E.Drivers;

namespace WebAPI.E2E.DataSources;

public class FeatureFlagDataSource(ConsoleDriver? _consoleDriver)
{
    private Dictionary<Tuple<string, string>, string> IdMapping = new();
}