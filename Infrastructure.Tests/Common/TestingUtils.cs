using Microsoft.EntityFrameworkCore;
using Moq;

namespace Infrastructure.Tests.Common;

public static class TestingUtils
{
    public static Mock<DbSet<T>> CreateDbSetMockFromList<T>(IEnumerable<T> data) where T : class
    {
        var queryableData = data.AsQueryable();

        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryableData.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryableData.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryableData.ElementType);
        // ReSharper disable once NotDisposedResourceIsReturned
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator())
            // ReSharper disable once NotDisposedResourceIsReturned
            .Returns(() => queryableData.GetEnumerator());

        return mockSet;
    }
}