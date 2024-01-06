using Application.Interactors.ListFeatureFlags;
using Domain.FeatureFlags;

namespace Application.Tests.UnitTests.Interactors.ListFeatureFlags;

public sealed class CodePresenterTests
{
    [Test]
    public void CodePresenter_Ok_Should_Store_FeatureFlags()
    {
        var models = new List<IModel>
        {
            new Model
            {
                Id = "some_flag",
                Enabled = true
            },
            new Model
            {
                Id = "another_flag",
                Enabled = false
            }
        };

        var presenter = new CodePresenter();

        presenter.Ok(models);

        Assert.That(presenter.FeatureFlags, Is.EqualTo(models));
    }
}