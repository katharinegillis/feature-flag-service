using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Infrastructure.Tests.Common;

[Category("Unit")]
public static class TestingUtils
{
    public static DbSet<T> CreateDbSetMockFromList<T>(IEnumerable<T> data) where T : class
    {
        var queryableData = data.AsQueryable();

        var set = Substitute.For<DbSet<T>, IQueryable<T>>();

        ((IQueryable<T>)set).Provider.Returns(queryableData.Provider);
        ((IQueryable<T>)set).Expression.Returns(queryableData.Expression);
        ((IQueryable<T>)set).ElementType.Returns(queryableData.ElementType);

        // ReSharper disable once GenericEnumeratorNotDisposed
        var queryableDataEnumerator = queryableData.GetEnumerator();
        // ReSharper disable once NotDisposedResource
        ((IQueryable<T>)set).GetEnumerator().Returns(queryableDataEnumerator);

        return set;
    }
}