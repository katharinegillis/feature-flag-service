using Domain.Common;
using Domain.FeatureFlags;

namespace Domain.Tests.UnitTests.FeatureFlags;

public class ModelTests
{
    [Test]
    public void FeatureFlag_Validate_Should_Pass_If_Id_Is_100_Characters()
    {
        var model = new Model
        {
            Id = "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuv",
            Enabled = true
        };

        var result = model.Validate();

        Assert.That(result.IsOk, Is.True);
    }

    [Test]
    public void FeatureFlag_Validate_Should_Fail_If_Id_Is_More_Than_100_Characters()
    {
        var model = new Model
        {
            Id =
                "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvw",
            Enabled = true
        };

        var result = model.Validate();
        Assert.Multiple(() =>
        {
            Assert.That(result.IsOk, Is.False);
            Assert.That(result.Error.Count, Is.EqualTo(1));
            Assert.That(result.Error.First(), Is.EqualTo(new ValidationError
            {
                Field = "Id",
                Message = "Max length is 100"
            }));
        });
    }
}