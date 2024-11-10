using Application.Interactors.FeatureFlag.Get;
using Domain.FeatureFlags;

namespace Application.Tests.Unit.Interactors.FeatureFlag.Get;

public sealed class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_FeatureFlag()
    {
        var model = new Model
        {
            Id = "some_flag",
            Enabled = true
        };

        var presenter = new CodePresenter();

        presenter.Ok(model);
        Assert.Multiple(() =>
        {
            Assert.That(presenter.FeatureFlag, Is.EqualTo(model));
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