using Domain.FeatureFlags;

namespace Domain.Tests.UnitTests.FeatureFlags;

public class FeatureFlagNullTests
{
    [Test]
    public void FeatureFlagNull_Should_Not_Allow_Enabled_To_Change()
    {
        var featureFlagNull = new FeatureFlagNull
        {
            Enabled = true
        };

        Assert.That(featureFlagNull.Enabled, Is.False);
    }

    [Test]
    public void FeatureFlagNull_Should_Return_Singleton_With_Instance()
    {
        var firstFeatureFlagNull = FeatureFlagNull.Instance;
        var secondFeatureFlagNull = FeatureFlagNull.Instance;
        Assert.That(ReferenceEquals(firstFeatureFlagNull, secondFeatureFlagNull));
    }

    [Test]
    public void FeatureFlagNull_Should_Equal_On_Same_Data()
    {
        var firstFeatureFlagNull = FeatureFlagNull.Instance;
        var secondFeatureFlagNull = FeatureFlagNull.Instance;
        Assert.That(firstFeatureFlagNull, Is.EqualTo(secondFeatureFlagNull));
    }
}