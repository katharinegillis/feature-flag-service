using Application.Interactors.IsFeatureFlagEnabled;

namespace Application.UnitTests.Interactors.IsFeatureFlagEnabled;

public class CodePresenterTests
{
    [Test]
    public void Ok_Should_Store_Enabled_Value()
    {
        var presenter = new CodePresenter();

        presenter.Ok(true);

        Assert.That(presenter.Enabled, Is.True);
        Assert.That(presenter.IsNotFound, Is.False);
    }

    [Test]
    public void NotFound_Should_Store_Enabled_As_Null()
    {
        var presenter = new CodePresenter();

        presenter.NotFound();

        Assert.That(presenter.IsNotFound, Is.True);
    }
}