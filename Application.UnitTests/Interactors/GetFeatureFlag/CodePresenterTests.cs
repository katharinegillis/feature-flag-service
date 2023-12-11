using Application.Interactors.GetFeatureFlag;
using Domain.FeatureFlag;

namespace Application.UnitTests.Interactors.GetFeatureFlag;

public class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_FeatureFlag()
    {
        var featureFlag = new FeatureFlag
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new CodePresenter();

        presenter.Ok(featureFlag);

        Assert.That(presenter.FeatureFlag, Is.EqualTo(featureFlag));
        Assert.That(presenter.IsNotFound, Is.False);
    }

    [Test]
    public void NotFound_Should_Store_FeatureFlagNull()
    {
        var presenter = new CodePresenter();

        presenter.NotFound();

        Assert.That(presenter.FeatureFlag, Is.TypeOf<FeatureFlagNull>());
        Assert.That(presenter.IsNotFound, Is.True);
    }
}