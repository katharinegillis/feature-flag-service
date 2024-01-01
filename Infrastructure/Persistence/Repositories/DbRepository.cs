using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using FeatureFlag = Infrastructure.Persistence.Models.FeatureFlag;

namespace Infrastructure.Persistence.Repositories;

public sealed class DbRepository(FeatureFlagContext context) : IRepository
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
        catch (UniqueConstraintException ex)
        {
            return new ValidationError
            {
                Field = "Id",
                Message = "Already exists"
            };
        }
        catch (DbUpdateException ex)
        {
            return new Error
            {
                Message = $"{ex.Message}; {ex.InnerException?.Message}"
            };
        }
    }
}