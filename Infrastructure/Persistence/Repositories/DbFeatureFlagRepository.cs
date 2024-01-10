using System.Data.Entity.Core;
using Domain.Common;
using Domain.FeatureFlags;
using EntityFramework.Exceptions.Common;
using Infrastructure.Persistence.Contexts;
using FeatureFlag = Infrastructure.Persistence.Models.FeatureFlag;

namespace Infrastructure.Persistence.Repositories;

public sealed class DbFeatureFlagRepository(FeatureFlagContext context, IFactory factory) : IRepository
{
    public Task<IModel> Get(string id)
    {
        var result = context
            .FeatureFlags
            .Where(e => e.FeatureFlagId == id)
            .Select(e => e)
            .SingleOrDefault();

        return Task.FromResult(result is not null
            ? factory.Create(result.FeatureFlagId, result.Enabled)
            : factory.Create());
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

    public Task<IEnumerable<IModel>> List()
    {
        return Task.FromResult(context.FeatureFlags.Select(e => factory.Create(e.FeatureFlagId, e.Enabled))
            .AsEnumerable());
    }

    public Task<Result<bool, Error>> Update(IModel model)
    {
        var featureFlag = new FeatureFlag
        {
            FeatureFlagId = model.Id,
            Enabled = model.Enabled
        };

        try
        {
            context.FeatureFlags.Update(featureFlag);

            context.SaveChanges();

            return Task.FromResult<Result<bool, Error>>(true);
        }
        catch (ObjectNotFoundException)
        {
            return Task.FromResult<Result<bool, Error>>(new NotFoundError());
        }
        catch (Exception ex)
        {
            return Task.FromResult<Result<bool, Error>>(new Error
            {
                Message = $"{ex.Message}{(ex.InnerException != null ? $"; {ex.InnerException?.Message}" : "")}"
            });
        }
    }
}