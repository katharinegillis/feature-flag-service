using FeatureFlagGet = Application.UseCases.FeatureFlag.Get;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Get;

[Parallelizable]
[Category("Unit")]
public sealed class CodePresenterFactoryTests
{
    [Test]
    public void CodePresenterFactory__Should_Be_An_ICodePresenterFactory()
    {
        // Act
        var subject = new FeatureFlagGet.CodePresenterFactory();

        // Assert
        Assert.That(subject, Is.InstanceOf<FeatureFlagGet.ICodePresenterFactory>());
    }

    [Test]
    public void CodePresenterFactory__Create__Returns_New_CodePresenter()
    {
        // Act
        var subject = new FeatureFlagGet.CodePresenterFactory();
        var result1 = subject.Create();
        var result2 = subject.Create();

        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(result1, Is.InstanceOf<FeatureFlagGet.ICodePresenter>());
            Assert.That(result1, Is.Not.SameAs(result2));
        });
    }
}