using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Infrastructure.Tests.Common;

public static class TestingUtils
{
    public static DbSet<T> CreateDbSetMockFromList<T>(IEnumerable<T> data) where T : class
    {
        var queryableData = data.AsQueryable();

        // var mockSet = new Mock<DbSet<T>>();
        // mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        // mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        // mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        // // ReSharper disable once NotDisposedResourceIsReturned
        // mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
        //     // ReSharper disable once NotDisposedResourceIsReturned
        //     .Returns(() => queryableData.GetEnumerator());
        var set = Substitute.For<DbSet<T>, IQueryable<T>>();
        ((IQueryable<T>)set).Provider.Returns(queryableData.Provider);
        ((IQueryable<T>)set).Expression.Returns(queryableData.Expression);
        ((IQueryable<T>)set).ElementType.Returns(queryableData.ElementType);
        // ReSharper disable once NotDisposedResource
        ((IQueryable<T>)set).GetEnumerator().Returns(queryableData.GetEnumerator());

        return set;
    }
}