using Domain.FeatureFlags;

namespace Domain.Tests.UnitTests.FeatureFlags;

public class FeatureFlagEqualityComparerTests
{
    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_True_If_Data_Is_The_Same()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var firstFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.True(comparer.Equals(firstFeatureFlag, secondFeatureFlag));
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_True_If_Both_Are_Null()
    {
        var comparer = new FeatureFlagEqualityComparer();

        Assert.True(comparer.Equals(null, null));
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_One_Is_Null()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var featureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };

        Assert.False(comparer.Equals(featureFlag, null));
        Assert.False(comparer.Equals(null, featureFlag));
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Id_Is_Different()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var firstFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new FeatureFlag
        {
            Id = "some_other_flag",
            Enabled = true,
        };

        Assert.False(comparer.Equals(firstFeatureFlag, secondFeatureFlag));
    }

    [Test]
    public void FeatureFlagEqualityComparer_Equals_Should_Return_False_If_Enabled_Is_Different()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var firstFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = false,
        };

        Assert.False(comparer.Equals(firstFeatureFlag, secondFeatureFlag));
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Same_If_Data_Is_Same()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var firstFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true,
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.EqualTo(comparer.GetHashCode(secondFeatureFlag)));
    }

    [Test]
    public void FeatureFlagEqualityComparer_GetHashCode_Should_Return_Different_If_Data_Is_Different()
    {
        var comparer = new FeatureFlagEqualityComparer();

        var firstFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };
        var secondFeatureFlag = new FeatureFlag
        {
            Id = "some_other_flag",
            Enabled = true,
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.Not.EqualTo(comparer.GetHashCode(secondFeatureFlag)));

        secondFeatureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = false
        };

        Assert.That(comparer.GetHashCode(firstFeatureFlag), Is.Not.EqualTo(comparer.GetHashCode(secondFeatureFlag)));
    }
}
