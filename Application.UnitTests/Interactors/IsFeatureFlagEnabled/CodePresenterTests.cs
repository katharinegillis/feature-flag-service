using Application.Interactors.IsFeatureFlagEnabled;

namespace Application.UnitTests.Interactors.IsFeatureFlagEnabled;

public class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_Enabled_Value_And_Not_Found_As_False()
    {
        var presenter = new CodePresenter();

        presenter.Ok(true);

        Assert.That(presenter.Enabled, Is.True);
        Assert.That(presenter.IsNotFound, Is.False);
    }

    [Test]
    public void NotFound_Should_Store_Not_Found_As_True()
    {
        var presenter = new CodePresenter();

        presenter.NotFound();

        Assert.That(presenter.IsNotFound, Is.True);
    }
}