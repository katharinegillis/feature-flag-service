using Domain.FeatureFlags;

namespace Domain.Tests.UnitTests.FeatureFlags;

public class EqualityComparerTests
{
    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_True_If_Data_Is_The_Same()
    {
        var comparer = new EqualityComparer();

        var firstFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.That(comparer.Equals(firstFeatureFlag, secondFeatureFlag), Is.True);
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

        var featureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        Assert.Multiple(() =>
        {
            Assert.That(comparer.Equals(featureFlag, null), Is.False);
            Assert.That(comparer.Equals(null, featureFlag), Is.False);
        });
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Id_Is_Different()
    {
        var comparer = new EqualityComparer();

        var firstFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new Model
        {
            Id = "some_other_flag",
            Enabled = true
        };

        Assert.That(comparer.Equals(firstFeatureFlag, secondFeatureFlag), Is.False);
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Enabled_Is_Different()
    {
        var comparer = new EqualityComparer();

        var firstFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = false
        };

        Assert.That(comparer.Equals(firstFeatureFlag, secondFeatureFlag), Is.False);
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Same_If_Data_Is_Same()
    {
        var comparer = new EqualityComparer();

        var firstFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.EqualTo(comparer.GetHashCode(secondFeatureFlag)));
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Different_If_Data_Is_Different()
    {
        var comparer = new EqualityComparer();

        var firstFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new Model
        {
            Id = "some_other_flag",
            Enabled = true
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.Not.EqualTo(comparer.GetHashCode(secondFeatureFlag)));

        secondFeatureFlag = new Model
        {
            Id = "some_flag",
            Enabled = false
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.Not.EqualTo(comparer.GetHashCode(secondFeatureFlag)));
    }
}