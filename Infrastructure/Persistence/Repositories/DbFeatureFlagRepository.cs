using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using FeatureFlag = Infrastructure.Persistence.Models.FeatureFlag;

namespace Infrastructure.Persistence.Repositories;

public sealed class DbFeatureFlagRepository(FeatureFlagContext context) : IRepository
{
    public Task<IModel> Get(string id)
    {
        var result = context
            .FeatureFlags
            .Where(e => e.FeatureFlagId == id)
            .Select(e => e)
            .SingleOrDefault();

        if (result is not null)
        {
            return Task.FromResult<IModel>(new Model
            {
                Id = result.FeatureFlagId,
                Enabled = result.Enabled
            });
        }

        return Task.FromResult<IModel>(NullModel.Instance);
    }

    public Task<Result<string, Error>> Create(IModel model)
    {
        var dbFeatureFlag = new FeatureFlag
        {
            FeatureFlagId = model.Id,
            Enabled = model.Enabled
        };

        try
        {
            context.FeatureFlags.Add(dbFeatureFlag);

            context.SaveChanges();

            return Task.FromResult<Result<string, Error>>(dbFeatureFlag.FeatureFlagId);
        }
        catch (UniqueConstraintException)
        {
            return Task.FromResult<Result<string, Error>>(new ValidationError
            {
                Field = "Id",
                Message = "Already exists"
            });
        }
        catch (Exception ex)
        {
            return Task.FromResult<Result<string, Error>>(new Error
            {
                Message = $"{ex.Message}{(ex.InnerException != null ? $"; {ex.InnerException?.Message}" : "")}"
            });
        }
    }
}