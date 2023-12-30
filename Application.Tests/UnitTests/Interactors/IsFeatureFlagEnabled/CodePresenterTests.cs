using Application.Interactors.IsFeatureFlagEnabled;

namespace Application.Tests.UnitTests.Interactors.IsFeatureFlagEnabled;

public sealed class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_Enabled_Value_And_Not_Found_As_False()
    {
        var presenter = new CodePresenter();

        presenter.Ok(true);
        Assert.Multiple(() =>
        {
            Assert.That(presenter.Enabled, Is.True);
            Assert.That(presenter.IsNotFound, Is.False);
        });
    }

    [Test]
    public void NotFound_Should_Store_Not_Found_As_True()
    {
        var presenter = new CodePresenter();

        presenter.NotFound();

        Assert.That(presenter.IsNotFound, Is.True);
    }
}