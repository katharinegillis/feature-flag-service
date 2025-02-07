using Domain.FeatureFlags;

namespace Domain.Tests.Unit.FeatureFlags;

[Category("Unit")]
public sealed class EqualityComparerTests
{
    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_True_If_Data_Is_The_Same()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var b = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.That(comparer.Equals(a, b), Is.True);
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_True_If_Both_Are_Null()
    {
        var comparer = new EqualityComparer();

        Assert.That(comparer.Equals(null, null), Is.True);
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_One_Is_Null()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        Assert.Multiple(() =>
        {
            Assert.That(comparer.Equals(a, null), Is.False);
            Assert.That(comparer.Equals(null, a), Is.False);
        });
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Id_Is_Different()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var b = new Model
        {
            Id = "some_other_flag",
            Enabled = true
        };

        Assert.That(comparer.Equals(a, b), Is.False);
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Enabled_Is_Different()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var b = new Model
        {
            Id = "some_flag",
            Enabled = false
        };

        Assert.That(comparer.Equals(a, b), Is.False);
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Same_If_Data_Is_Same()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var b = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.That(comparer.GetHashCode(a), Is.EqualTo(comparer.GetHashCode(b)));
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Different_If_Data_Is_Different()
    {
        var comparer = new EqualityComparer();

        var a = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var b = new Model
        {
            Id = "some_other_flag",
            Enabled = true
        };

        Assert.That(comparer.GetHashCode(a), Is.Not.EqualTo(comparer.GetHashCode(b)));

        b = new Model
        {
            Id = "some_flag",
            Enabled = false
        };

        Assert.That(comparer.GetHashCode(a), Is.Not.EqualTo(comparer.GetHashCode(b)));
    }
}