using Domain.FeatureFlag;

namespace Domain.UnitTests.FeatureFlag;

public class FeatureFlagNullTests
{
    [Test]
    public void FeatureFlagNull_Should_Not_Allow_Id_To_Change()
    {
        var featureFlagNull = new FeatureFlagNull();

        featureFlagNull.Id = "some_flag";

        Assert.That(featureFlagNull.Id, Is.EqualTo(""));
    }

    [Test]
    public void FeatureFlagNull_Should_Not_Allow_Enabled_To_Change()
    {
        var featureFlagNull = new FeatureFlagNull();

        featureFlagNull.Enabled = true;

        Assert.That(featureFlagNull.Enabled, Is.False);
    }

    [Test]
    public void FeatureFlagNull_Should_Return_Singleton_With_Instance()
    {
        var firstFeatureFlagNull = FeatureFlagNull.Instance;
        var secondFeatureFlagNull = FeatureFlagNull.Instance;
        Assert.That(firstFeatureFlagNull, Is.EqualTo(secondFeatureFlagNull));
    }
}