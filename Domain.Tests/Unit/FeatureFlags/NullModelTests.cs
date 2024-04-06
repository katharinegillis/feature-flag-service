using Domain.Common;
using Domain.FeatureFlags;

namespace Domain.Tests.Unit.FeatureFlags;

[Category("Unit")]
public sealed class NullModelTests
{
    [Test]
    public void FeatureFlagNull_Should_Not_Allow_Enabled_To_Change()
    {
        var nullModel = new NullModel
        {
            Enabled = true
        };

        Assert.That(nullModel.Enabled, Is.False);
    }

    [Test]
    public void FeatureFlagNull_Should_Return_Singleton_With_Instance()
    {
        var a = NullModel.Instance;
        var b = NullModel.Instance;
        Assert.That(ReferenceEquals(a, b));
    }

    [Test]
    public void FeatureFlagNull_Should_Equal_On_Same_Data()
    {
        var a = NullModel.Instance;
        var b = NullModel.Instance;
        Assert.That(a, Is.EqualTo(b));
    }

    [Test]
    public void FeatureFlagNull_Should_Not_Let_Id_Be_Set()
    {
        var nullModel = new NullModel
        {
            Id = "some_flag"
        };

        Assert.That(nullModel.Id, Is.EqualTo(""));
    }

    [Test]
    public void FeatureFlagNull_Validate_Should_Return_Is_Null_Error()
    {
        var nullModel = NullModel.Instance;

        var result = nullModel.Validate();
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