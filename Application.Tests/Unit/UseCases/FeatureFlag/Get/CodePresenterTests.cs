using NSubstitute;
using FeatureFlagGet = Application.UseCases.FeatureFlag.Get;
using FeatureFlags = Domain.FeatureFlags;

namespace Application.Tests.Unit.UseCases.FeatureFlag.Get;

[Parallelizable]
[Category("Unit")]
public sealed class CodePresenterTests
{
    [Test]
    public void CodePresenter__Ok__Stores_FeatureFlag_And_IsNotFound_False()
    {
        // Arrange
        var model = Substitute.For<FeatureFlags.IModel>();

        // Act
        var subject = new FeatureFlagGet.CodePresenter();
        subject.Ok(model);

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.FeatureFlag, Is.SameAs(model));
            Assert.That(subject.IsNotFound, Is.False);
        }
    }

    [Test]
    public void CodePresenter__NotFound__Stores_FeatureFlag_And_IsNotFound_True()
    {
        // Act
        var subject = new FeatureFlagGet.CodePresenter();
        subject.NotFound();

        // Assert
        using (Assert.EnterMultipleScope())
        {
            Assert.That(subject.FeatureFlag, Is.TypeOf<FeatureFlags.NullModel>());
            Assert.That(subject.IsNotFound, Is.True);
        }
    }
}