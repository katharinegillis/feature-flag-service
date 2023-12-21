using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlags;

namespace Application.Tests.UnitTests.Interactors.GetFeatureFlag;

public class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_FeatureFlag()
    {
        var featureFlag = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new CodePresenter();

        presenter.Ok(featureFlag);
        Assert.Multiple(() =>
        {
            Assert.That(presenter.FeatureFlag, Is.EqualTo(featureFlag));
            Assert.That(presenter.IsNotFound, Is.False);
        });
    }

    [Test]
    public void NotFound_Should_Store_FeatureFlagNull()
    {
        var presenter = new CodePresenter();

        presenter.NotFound();
        Assert.Multiple(() =>
        {
            Assert.That(presenter.FeatureFlag, Is.TypeOf<NullModel>());
            Assert.That(presenter.IsNotFound, Is.True);
        });
    }
}