using Domain.Common;
using Domain.FeatureFlags;

namespace Domain.Tests.UnitTests.FeatureFlags;

public class NullModelTests
{
    [Test]
    public void FeatureFlagNull_Should_Not_Allow_Enabled_To_Change()
    {
        var featureFlagNull = new NullModel
        {
            Enabled = true
        };

        Assert.That(featureFlagNull.Enabled, Is.False);
    }

    [Test]
    public void FeatureFlagNull_Should_Return_Singleton_With_Instance()
    {
        var firstFeatureFlagNull = NullModel.Instance;
        var secondFeatureFlagNull = NullModel.Instance;
        Assert.That(ReferenceEquals(firstFeatureFlagNull, secondFeatureFlagNull));
    }

    [Test]
    public void FeatureFlagNull_Should_Equal_On_Same_Data()
    {
        var firstFeatureFlagNull = NullModel.Instance;
        var secondFeatureFlagNull = NullModel.Instance;
        Assert.That(firstFeatureFlagNull, Is.EqualTo(secondFeatureFlagNull));
    }

    [Test]
    public void FeatureFlagNull_Should_Not_Let_Id_Be_Set()
    {
        var featureFlagNull = new NullModel
        {
            Id = "some_flag"
        };

        Assert.That(featureFlagNull.Id, Is.EqualTo(""));
    }

    [Test]
    public void FeatureFlagNull_Validate_Should_Return_Is_Null_Error()
    {
        var featureFlagNull = NullModel.Instance;

        var result = featureFlagNull.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error.Count, Is.EqualTo(1));
            Assert.That(result.Error.First(), Is.EqualTo(new ValidationError
            {
                Field = "Id",
                Message = "Null object"
            }));
        });
    }
}