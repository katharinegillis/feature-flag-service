using EntityFramework.Exceptions.Sqlite;
using Infrastructure.Persistence.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Tests.IntegrationTests.Repositories;

public abstract class BaseFeatureFlagContextRepositoryTests
{
    public SqliteConnection GetSqliteConnection()
    {
        if (File.Exists("test.db"))
        {
            File.Delete("test.db");
        }

        File.Create("test.db").Close();

        var connection = new SqliteConnection("Data Source=test.db");
        connection.Open();

        return connection;
    }

    public DbContextOptions<FeatureFlagContext> GetSqliteContextOptions(SqliteConnection connection)
    {
        var options = new DbContextOptionsBuilder<FeatureFlagContext>()
            .UseSqlite(connection)
            .UseExceptionProcessor()
            .Options;

        using (var context = new FeatureFlagContext(options))
        {
            context.Database.EnsureCreated();
        }

        return options;
    }

    public void CloseSqliteConnection(SqliteConnection connection)
    {
        connection.Close();

        if (File.Exists("test.db"))
        {
            File.Delete("test.db");
        }
    }
}