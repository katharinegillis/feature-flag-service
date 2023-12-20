using Domain.Common;
using Domain.FeatureFlags;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using FeatureFlag = Infrastructure.Persistence.Models.FeatureFlag;

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

    public async Task<Result<string, Error>> Create(IFeatureFlag featureFlag)
    {
        var dbFeatureFlag = new FeatureFlag
        {
            FeatureFlagId = featureFlag.Id,
            Enabled = featureFlag.Enabled
        };

        try
        {
            var entityEntry = context.FeatureFlags.Add(dbFeatureFlag);

            await context.SaveChangesAsync();

            return entityEntry.Entity.FeatureFlagId;
        }
        catch (InvalidOperationException ex)
        {
            if (ex.Message ==
                "The instance of entity type 'FeatureFlag' cannot be tracked because another instance with the same key value for {'FeatureFlagId'} is already being tracked. When attaching existing entities, ensure that only one entity instance with a given key value is attached. Consider using 'DbContextOptionsBuilder.EnableSensitiveDataLogging' to see the conflicting key values.")
            {
                return new ValidationError
                {
                    Field = "Id",
                    Message = "Id already exists"
                };
            }

            return new Error
            {
                Message = ex.Message
            };
        }
    }
}