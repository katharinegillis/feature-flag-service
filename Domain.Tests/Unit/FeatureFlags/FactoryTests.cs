using Domain.FeatureFlags;

namespace Domain.Tests.Unit.FeatureFlags;

[Parallelizable]
[Category("Unit")]
public sealed class FactoryTests
{
    [Test]
    public void Create_With_Data_Returns_New_Model()
    {
        var factory = new Factory();

        var model = factory.Create("new_flag", true);

        using (Assert.EnterMultipleScope())
        {
            Assert.That(model, Is.InstanceOf<Model>());
            Assert.That(model.Id, Is.EqualTo("new_flag"));
            Assert.That(model.Enabled, Is.True);
            Assert.That(model.IsNull, Is.False);
        }
    }

    [Test]
    public void Create_With_No_Data_Returns_The_Null_Model()
    {
        var factory = new Factory();

        var model = factory.Create();

        using (Assert.EnterMultipleScope())
        {
            Assert.That(model, Is.InstanceOf<NullModel>());
            Assert.That(model.IsNull, Is.True);
            Assert.That(model, Is.EqualTo(NullModel.Instance));
        }
    }
}