using WebAPI.E2E.Drivers;
using RandomString4Net;

namespace WebAPI.E2E.DataSources;

public class FeatureFlagDataSource(ConsoleDriver consoleDriver)
{
    private readonly string _key = RandomString.GetString(Types.ALPHANUMERIC_LOWERCASE, 10);

    public async Task<string> GetUniqueId(string id)
    {
        return await consoleDriver.ConfigShowDataSource() == "Database" ? $"{_key}_{id}" : id;
    }

    public async Task<bool> CreateFeatureFlag(string id, bool enabled)
    {
        return await consoleDriver.CreateFeatureFlag(await GetUniqueId(id), enabled);
    }

    public async Task<bool> DeleteFeatureFlag(string id)
    {
        return await consoleDriver.DeleteFeatureFlag(await GetUniqueId(id));
    }
}