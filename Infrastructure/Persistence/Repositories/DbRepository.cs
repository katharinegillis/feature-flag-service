using Domain.Common;
using Domain.FeatureFlags;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using FeatureFlag = Infrastructure.Persistence.Models.FeatureFlag;

namespace Infrastructure.Persistence.Repositories;

public class DbRepository(FeatureFlagContext context) : IRepository
{
    public async Task<IModel> Get(string id)
    {
        var result = await context
            .FeatureFlags
            .Where(e => e.FeatureFlagId == id)
            .Select(e => e)
            .SingleOrDefaultAsync()
            .ConfigureAwait(false);

        if (result is not null)
        {
            return new Model
            {
                Id = result.FeatureFlagId,
                Enabled = result.Enabled
            };
        }

        return NullModel.Instance;
    }

    public async Task<Result<string, Error>> Create(IModel model)
    {
        var dbFeatureFlag = new FeatureFlag
        {
            FeatureFlagId = model.Id,
            Enabled = model.Enabled
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