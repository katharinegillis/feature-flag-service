using Infrastructure.Persistence.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.IntegrationTests.Repositories;

public abstract class BaseFeatureFlagContextRepositoryTests
{
    public SqliteConnection GetSqliteConnection()
    {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
    }

    public DbContextOptions<FeatureFlagContext> GetSqliteContextOptions(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<FeatureFlagContext>()
            .UseSqlite(connection)
            .Options;

        using (var context = new FeatureFlagContext(options))
        {
            context.Database.EnsureCreated();
        }

        return options;
    }
}