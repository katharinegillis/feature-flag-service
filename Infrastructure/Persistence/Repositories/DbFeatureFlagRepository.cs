using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class DbFeatureFlagRepository(FeatureFlagContext context) : Domain.FeatureFlags.IFeatureFlagRepository
{
    public async Task<Domain.FeatureFlags.IFeatureFlag> Get(string id)
    {
        var result = await context
            .FeatureFlags
            .Where(e => e.FeatureFlagId == id)
            .Select(e => e)
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);

        if (result is not null)
        {
            return new Domain.FeatureFlags.FeatureFlag
            {
                Id = result.FeatureFlagId,
                Enabled = result.Enabled
            };
        }

        return Domain.FeatureFlags.FeatureFlagNull.Instance;
    }
}